using System.Text.Json.Serialization;

namespace Mockingbird.API.DTO;

public record ApiResourceDTO
{
    [JsonPropertyName("carrier_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? CarrierId { get; init; }
    
    [JsonPropertyName("api_resource_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? ApiResourceId { get; init; }
    
    [JsonPropertyName("name")]
    public string Name { get; init; }
    
    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("methods")]
    public ICollection<MethodDTO> Methods { get; set; } 
}
