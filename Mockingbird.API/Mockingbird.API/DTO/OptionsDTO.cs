using System.Text.Json.Serialization;

namespace Mockingbird.API.DTO;

public record OptionDTO
{   
    [JsonPropertyName("carrier_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? CarrierId { get; init; }
    
    [JsonPropertyName("option_id")]
    public int? OptionId { get; init; }
    
    [JsonPropertyName("name")]
    public string Name { get; init; }
    
    [JsonPropertyName("value")]
    public string Value { get; set; }
}
