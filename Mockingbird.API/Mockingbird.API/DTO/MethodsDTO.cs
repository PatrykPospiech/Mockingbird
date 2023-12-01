using System.Text.Json.Serialization;

namespace Mockingbird.API.DTO;

public record MethodDTO
{
    [JsonPropertyName("method_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? MethodId { get; init; }
    
    [JsonPropertyName("name")]
    public string Name { get; init; }
    
    [JsonPropertyName("method_type")]
    public string? MethodType { get; set; }
    
    [JsonPropertyName("responses")]
    public ICollection<ResponseDTO>? Responses { get; set; }
}
