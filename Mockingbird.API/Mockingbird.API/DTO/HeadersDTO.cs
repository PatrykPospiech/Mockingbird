using System.Text.Json.Serialization;

namespace Mockingbird.API.DTO;

public record HeaderDTO
{
    [JsonPropertyName("header_id")]
    public int? HeaderId { get; init; }
    
    [JsonPropertyName("name")]
    public string Name { get; init; }
    
    [JsonPropertyName("value")]
    public string Value { get; set; }
}
