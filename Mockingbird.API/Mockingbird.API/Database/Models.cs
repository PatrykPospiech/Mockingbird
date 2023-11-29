namespace Mockingbird.API.Database;

using System.Collections.Generic;

public record Carrier
{
    public int CarrierId { get; init; }
    public string Name { get; init; }
    public string Nickname { get; init; }
    public byte[] Icon { get; init; }
    
    public ICollection<Option> Options { get; init; }
    public ICollection<ApiResource> ApiResources { get; init; }
}

public record Option
{
    public int OptionId { get; init; }
    public string Name { get; init; }
    public string Value { get; init; }
    
    public int CarrierId { get; init; }
    public Carrier Carrier { get; init; }
}

public record ApiResource
{
    public int ApiResourceId { get; init; }
    public string Name { get; init; }
    public string Url { get; init; }
    
    public int CarrierId { get; init; }
    public Carrier Carrier { get; init; }
    public ICollection<Method> Methods { get; init; }
}

public record Method
{
    public int MethodId { get; init; }
    public string Name { get; init; }
    public string MethodType { get; init; }
    
    public int ApiResourceId { get; init; }
    public ApiResource ApiResource { get; init; }
    public ICollection<Response> Responses { get; init; }
}

public record Response
{
    public string ResponseId { get; init; }
    public bool IsActive { get; init; }
    public string ResponseStatusCode { get; init; }
    public string ResponseBody { get; init; }
    
    public int MethodId { get; init; }
    public Method Method { get; init; }
    public ICollection<Header> Headers { get; init; }
}

public record Header
{
    public int HeaderId { get; init; }
    public string Name { get; init; }
    public string Value { get; init; }
    
    public int ResponseId { get; init; }
    public Response Response { get; init; }
}
