namespace Mockingbird.API;

public class DataSource
{
    public static Dictionary<string, string> TestDynamicEndpoints = new Dictionary<string, string>()
    {
        { "rates", "test rates response"}
    };
    
    public static KeyValuePair<string, string> GetResponse(string s)
    {
        return DataSource.TestDynamicEndpoints.FirstOrDefault(i => i.Key == s);
    }
}