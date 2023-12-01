using System.Text.Json.Serialization;
using Mockingbird.API.Database;

namespace Mockingbird.API.DTO;

public record CarrierDTO
{
    [JsonPropertyName("carrier_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? CarrierId { get; init; }
    
    [JsonPropertyName("name")]
    public string Name { get; init; }
    
    [JsonPropertyName("nickname")]
    public string Nickname { get; init; }
    
    [JsonPropertyName("icon")]
    public string? Icon { get; init; }
    
    [JsonPropertyName("options")]
    public ICollection<OptionDTO>? Options { get; init; }
    
    [JsonPropertyName("api_resources")]
    public ICollection<ApiResourceDTO>? ApiResources { get; init; }
}
