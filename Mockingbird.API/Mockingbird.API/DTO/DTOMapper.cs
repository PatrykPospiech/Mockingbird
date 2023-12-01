using System.Text;
using Mockingbird.API.Database;

namespace Mockingbird.API.DTO;

public class DTOMapper
{
    public static CarrierDTO MapCarrierToDTO(Carrier carrier)
    {
        return new CarrierDTO
        {
            CarrierId = carrier.CarrierId,
            Name = carrier.Name,
            Nickname = carrier.Nickname,
            Icon = carrier.Icon != null ? Encoding.UTF8.GetString(carrier.Icon) : null,
            Options = MapOptionsToDTO(carrier.Options),
            ApiResources = MapApiResourcesToDTO(carrier.ApiResources)
        };
    }

    public static OptionDTO MapOptionToDTO(Option option)
    {
        return new OptionDTO
        {
            OptionId = option.OptionId,
            Name = option.Name,
            Value = option.Value
        };
    }
    
    public static List<OptionDTO> MapOptionsToDTO(ICollection<Option> options)
    {
        return options?
            .Select(MapOptionToDTO)
            .ToList();
    }

    public static ApiResourceDTO MapApiResourceToDTO(ApiResource apiResource)
    {
        return new ApiResourceDTO
        {
            ApiResourceId = apiResource.ApiResourceId,
            Name = apiResource.Name,
            Url = apiResource.Url,
            Methods = MapMethodsToDTO(apiResource.Methods)
        };
    }

    public static List<ApiResourceDTO> MapApiResourcesToDTO(ICollection<ApiResource> apiResources)
    {
        return apiResources?
            .Select(MapApiResourceToDTO)
            .ToList();
    }

    public static List<MethodDTO> MapMethodsToDTO(ICollection<Method> methods)
    {
        return methods?.Select(method => new MethodDTO
        {
            MethodId = method.MethodId,
            Name = method.Name,
            MethodType = method.MethodType,
            Responses = MapResponsesToDTO(method.Responses)
        }).ToList();
    }

    public static List<ResponseDTO> MapResponsesToDTO(ICollection<Response> responses)
    {
        return responses?.Select(response => new ResponseDTO
        {
            ResponseId = response.ResponseId,
            IsActive = response.IsActive,
            ResponseStatusCode = response.ResponseStatusCode,
            ResponseBody = response.ResponseBody,
            Headers = MapHeadersToDTO(response.Headers)
        }).ToList();
    }

    public static List<HeaderDTO> MapHeadersToDTO(ICollection<Header> headers)
    {
        return headers?.Select(header => new HeaderDTO
        {
            HeaderId = header.HeaderId,
            Name = header.Name,
            Value = header.Value
        }).ToList();
    }
}