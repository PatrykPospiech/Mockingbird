using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mockingbird.API.Database;
using Mockingbird.API.DTO;

namespace Mockingbird.API.Controllers;

public class ResourcesController: BaseController
{
    public ResourcesController(CarrierContext carrierContext) : base(carrierContext)
    {
    }

    [HttpGet]
    public async Task<IResult> GetResources(int carrierId = -1, int apiResourceId = -1)
    {
        IQueryable<ApiResource> query = _carrierContext.ApiResources.AsQueryable()
            .Include(apiResource => apiResource.Methods)
            .ThenInclude(method => method.Responses)
            .ThenInclude(response => response.Headers);

        if (carrierId > -1)
        {
            query = query.Where(resource => resource.CarrierId == carrierId);
        }

        if (apiResourceId > -1)
        {
            query = query.Where(resource => resource.ApiResourceId == apiResourceId);
        }

        var result = await query.ToListAsync();
        
        if (result.Count == 0)
        {
            return TypedResults.NotFound("ApiResources not found");
        }
        
        return TypedResults.Ok(DTOMapper.MapApiResourcesToDTO(result));
    }

    [HttpPost]
    public async Task<IResult> PostResource(int carrierId, ApiResourceDTO resource)
    {
        try
        {
            if (carrierId == null || carrierId == 0)
            {
                return TypedResults.BadRequest("No carrier id provided in query");
            }
            
            if (resource == null)
            {
                return TypedResults.BadRequest();
            }

            var carrier = await _carrierContext.Carriers.Include(carrier => carrier.ApiResources)
                .FirstOrDefaultAsync(carrier => carrier.CarrierId == carrierId);

            if (carrier == null)
            {
                return TypedResults.NotFound(
                    $"The provided carrier with id: \'{resource.CarrierId}\' couldn't be found.");
            }

            var apiResource = new ApiResource 
            {
                Name = resource.Name,
                Url = resource.Url,
                Methods = resource.Methods?.Select(method => new Method()
                {
                    Name = method.Name,
                    MethodType = method.MethodType,
                    Responses = method.Responses.Select(response => new Response()
                    {
                        ResponseId = Guid.NewGuid().ToString(),
                        ResponseBody = response.ResponseBody,
                        ResponseStatusCode = response.ResponseStatusCode,
                        IsActive = response.IsActive ?? false,
                        Headers = response.Headers.Select(header => new Header()
                        {
                            Name = header.Name,
                            Value = header.Value
                        }).ToList() ?? new List<Header>()
                    }).ToList() ?? new List<Response>()

                }).ToList() ?? new List<Method>()
            };
            
            carrier.ApiResources.Add(apiResource);
            await _carrierContext.SaveChangesAsync();

            return TypedResults.Ok(DTOMapper.MapApiResourceToDTO(apiResource));
        }
        catch (Exception ex)
        {
            return TypedResults.Problem(ex.ToString());
        }
    }
}