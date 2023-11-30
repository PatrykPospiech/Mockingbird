using Mockingbird.API.Database;

namespace Mockingbird.API.DTO;

public record CarrierInputDTO
{
    public string Name { get; init; }
    public string Nickname { get; init; }
    public byte[]? Icon { get; init; }
}

public record CarrierOutputDTO
{
    public int CarrierId { get; init; }
    public string Name { get; init; }
    public string Nickname { get; init; }
    public byte[]? Icon { get; init; }
    
    public ICollection<Option>? Options { get; init; }
    public ICollection<ApiResource>? ApiResources { get; init; }
}
