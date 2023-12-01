using System.Text;
using Mockingbird.API.Logic;

namespace Mockingbird.API.ReverseProxy
{
  public class ProxyMiddleware
  {
    private static readonly HttpClient _httpClient = new HttpClient();
    private readonly RequestDelegate _nextMiddleware;
    private readonly ILogger<ProxyMiddleware> _logger;
    
    public ProxyMiddleware(RequestDelegate nextMiddleware, ILogger<ProxyMiddleware> logger)
    {
      _nextMiddleware = nextMiddleware;
      _logger = logger;
    }
    
    public async Task Invoke(HttpContext context)
    {
      _logger.LogInformation("ReverseProxyMiddleware");

      if (!context.Request.Path.Value.Contains("mock"))
      {
        await _nextMiddleware(context);
        return;
      }
      
      var targetUri = BuildTargetUri(context.Request);

      if (targetUri == null)
      {
        await _nextMiddleware(context); 
        return;
      }

      _logger.LogInformation("Map proxy request to target");
      var targetRequestMessage = CreateTargetMessage(context, targetUri);
      
      using var responseMessage = await _httpClient.SendAsync(targetRequestMessage, HttpCompletionOption.ResponseHeadersRead, context.RequestAborted);
      context.Response.StatusCode = (int)responseMessage.StatusCode;
      
      
      CopyFromTargetResponseHeaders(context, responseMessage);
      await responseMessage.Content.CopyToAsync(context.Response.Body);
      
      _logger.LogInformation("Save files...");
      var originalRequestBodyStream = context.Request.Body;
      using (StreamReader reader = new StreamReader(originalRequestBodyStream, Encoding.UTF8))
      {
        string content = await reader.ReadToEndAsync();
        IOFilesHelper.SaveFile(content, "request", _logger); 
        // TODO: looking for solution to identify labels for specific carrier.
        // - populate carrier name into headers to save files in specific directory ?? 
        // - get carrier name based on the path, save request response by gui and connect it on the db - specific carrier collection of the requests
        // - or just save all requests and responses to the database
      }
      
      var responseBodyStream = context.Response.Body;
      using (StreamReader reader = new StreamReader(responseBodyStream, Encoding.UTF8))
      {
        string content = await reader.ReadToEndAsync();
        IOFilesHelper.SaveFile(content, "response", _logger);
      }
    }

    private HttpRequestMessage CreateTargetMessage(HttpContext context, Uri targetUri)
    {
      var requestMessage = new HttpRequestMessage();
      CopyFromOriginalRequestContentAndHeaders(context, requestMessage);

      requestMessage.RequestUri = targetUri;
      requestMessage.Headers.Host = targetUri.Host;
      requestMessage.Method = GetMethod(context.Request.Method);

      return requestMessage;
    }

    private void CopyFromOriginalRequestContentAndHeaders(HttpContext context, HttpRequestMessage requestMessage)
    {
      var requestMethod = context.Request.Method;

      if (!HttpMethods.IsGet(requestMethod) &&
        !HttpMethods.IsHead(requestMethod) &&
        !HttpMethods.IsDelete(requestMethod) &&
        !HttpMethods.IsTrace(requestMethod))
      {
        var streamContent = new StreamContent(context.Request.Body);
        requestMessage.Content = streamContent;
      }

      foreach (var header in context.Request.Headers)
      {
        requestMessage.Content?.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
      }
    }

    private void CopyFromTargetResponseHeaders(HttpContext context, HttpResponseMessage responseMessage)
    {
      foreach (var header in responseMessage.Headers)
      {
        context.Response.Headers[header.Key] = header.Value.ToArray();
      }

      foreach (var header in responseMessage.Content.Headers)
      {
        context.Response.Headers[header.Key] = header.Value.ToArray();
      }
      context.Response.Headers.Remove("transfer-encoding");
    }
    private static HttpMethod GetMethod(string method)
    {
      if (HttpMethods.IsDelete(method)) return HttpMethod.Delete;
      if (HttpMethods.IsGet(method)) return HttpMethod.Get;
      if (HttpMethods.IsHead(method)) return HttpMethod.Head;
      if (HttpMethods.IsOptions(method)) return HttpMethod.Options;
      if (HttpMethods.IsPost(method)) return HttpMethod.Post;
      if (HttpMethods.IsPut(method)) return HttpMethod.Put;
      if (HttpMethods.IsTrace(method)) return HttpMethod.Trace;
      return new HttpMethod(method);
    }

    //It will be consumed from the configuration later...
    private Dictionary<string, string> UriMapper = new Dictionary<string, string>()
    {
      { "/mock/dhl", "dhl.com/createlabel" },
      { "/mock/test", "google.com/maps"}
    };
    
    private Uri BuildTargetUri(HttpRequest request)
    {
      _logger.LogInformation($"Looking for target URI: {request.Path}");

      var mapping = UriMapper.FirstOrDefault(i => request.Path.StartsWithSegments(i.Key, out _));
      if (mapping.Value != null)
      {
        _logger.LogInformation("Target URI has been found");
        return new Uri("http://www." + mapping.Value);
      }

      return null;
    }
  }
}