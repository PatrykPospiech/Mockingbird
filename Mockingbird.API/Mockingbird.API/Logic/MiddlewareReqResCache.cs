using Microsoft.Extensions.Caching.Memory;

namespace Mockingbird.API.Logic;

public class MiddlewareReqResCache
{
    private static readonly Dictionary<int, RequestResponseData> Cache = new Dictionary<int, RequestResponseData>();
    
    public void AddToCache(int key, RequestResponseData value)
    {
        Cache.Add(key, value);
    }
    
    public void AddNextElement(RequestResponseData value)
    {
        Cache.Add(Cache.Count + 1, value);
    }


    public object GetFromCache(int key)
    {
        return Cache[key];
    }
}

// I have an issue to inject db context into middleware - it might happen only for Scoped services and I don't want to do that
// My proposition is to use MemoryCache to store data that will be processed later to db and use cache it self in middleware
public class RequestResponseData
{
    public int CarrierId { get; set; }
    public string Url { get; set; }
    public string RequestBase64 { get; set; }
    public string ResponseBase64 { get; set; }
}

// Scoped service doesn't work as well. I have to use singleton to store data in memory and /mock/* will be save data