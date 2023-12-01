using System.Text.Json.Serialization;

namespace Mockingbird.API.DTO;

public record ResponseDTO
{
    [JsonPropertyName("response_id")]
    public string? ResponseId { get; init; }
    
    [JsonPropertyName("is_active")]
    public bool? IsActive { get; set; }
    
    [JsonPropertyName("response_status_code")]
    public string? ResponseStatusCode { get; set; }
    
    [JsonPropertyName("response_body")]
    public string? ResponseBody { get; set; }
    
    [JsonPropertyName("headers")]
    public ICollection<HeaderDTO>? Headers { get; set; }
}
