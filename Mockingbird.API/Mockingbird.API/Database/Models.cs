using Microsoft.EntityFrameworkCore;

namespace Mockingbird.API.Database;

using System.Collections.Generic;

[Index(nameof(Name), nameof(Nickname), IsUnique = true)]
public record Carrier
{
    public int CarrierId { get; init; }
    public string Name { get; init; }
    public string Nickname { get; init; }
    public byte[]? Icon { get; init; }
    
    public ICollection<Option>? Options { get; set; }
    public ICollection<ApiResource>? ApiResources { get; set; }
    public ICollection<TPSCommunication>? TpsCommunications { get; set; }
}

[Index(nameof(CarrierId), nameof(Name), nameof(Value), IsUnique = true)]
public record Option
{
    public int OptionId { get; init; }
    public string Name { get; init; }
    public string Value { get; set; }
    
    public int CarrierId { get; init; }
    public Carrier Carrier { get; init; }
}

public record ApiResource
{
    public int ApiResourceId { get; init; }
    public string Name { get; init; }
    public string Url { get; set; }
    
    public int CarrierId { get; init; }
    public Carrier Carrier { get; init; }
    public ICollection<Method>? Methods { get; set; }
}

[Index(nameof(ApiResourceId), nameof(Name), nameof(MethodType), IsUnique = true)]
public record Method
{
    public int MethodId { get; init; }
    public string Name { get; init; }
    public string MethodType { get; set; }
    
    public int ApiResourceId { get; init; }
    public ApiResource ApiResource { get; init; }
    public ICollection<Response>? Responses { get; set; }
}

public record Response
{
    public string ResponseId { get; init; }
    public bool IsActive { get; set; }
    public string? ResponseStatusCode { get; set; }
    public string ResponseBody { get; set; }
    
    public int MethodId { get; init; }
    public Method Method { get; init; }
    public ICollection<Header>? Headers { get; set; }
}

public record Header
{
    public int HeaderId { get; init; }
    public string Name { get; init; }
    public string Value { get; set; }
    
    public int ResponseId { get; init; }
    public Response Response { get; init; }
}

public record TPSCommunication
{
    public int Id { get; init; }
    public string RequestBase64 { get; set; }
    public string ResponseBase64 { get; set; }
}