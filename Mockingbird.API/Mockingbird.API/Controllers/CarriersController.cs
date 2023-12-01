using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mockingbird.API.Database;
using Mockingbird.API.DTO;

namespace Mockingbird.API.Controllers;

public class CarriersController : BaseController
{
    public CarriersController(CarrierContext carrierContext) : base(carrierContext)
    {
    }

    [HttpGet]
    public async Task<IResult> GetCarriers(int carrierId = -1)
    {
        IQueryable<Carrier> query = _carrierContext.Carriers.AsQueryable()
            .Include(carrier => carrier.Options)
            .Include(carrier => carrier.TpsCommunications)
            .Include(carrier => carrier.ApiResources)
            .ThenInclude(apiResource => apiResource.Methods)
            .ThenInclude(method => method.Responses)
            .ThenInclude(response => response.Headers);

        if (carrierId > -1)
        {
            query = query.Where(carrier => carrier.CarrierId == carrierId);
        }

        var result = await query.ToListAsync();
        if (result.Count == 0)
        {
            return TypedResults.NotFound(carrierId > -1 ? $"Carrier with id: \'{carrierId}\' not found." : "No carriers found");
        }
        
        return TypedResults.Ok(result.Select(DTOMapper.MapCarrierToDTO));
    }

    [HttpPost]
    public async Task<IResult> PostCarrier(CarrierDTO carrier)
    {
        try
        {
            if (carrier == null)
            {
                return TypedResults.BadRequest();
            }
            
            var icon = carrier.Icon != null ? Convert.FromBase64String(carrier.Icon) : null;
            var options = carrier.Options.Select(option => new Option{ Name = option.Name, Value = option.Value }).ToList();
            
            var apiResources = carrier.ApiResources?.Select(apiResource => new ApiResource
            {
                Name = apiResource.Name,
                Url = apiResource.Url,
                Methods = apiResource.Methods?.Select(method => new Method
                {
                    Name = method.Name,
                    MethodType = method.MethodType,
                    Responses = method.Responses?.Select(response => new Response
                    {
                        ResponseId = Guid.NewGuid(),
                        IsActive = response.IsActive ?? false,
                        ResponseStatusCode = response.ResponseStatusCode,
                        ResponseBody = response.ResponseBody,
                        Headers = response.Headers?.Select(header => new Header
                        {
                            Name = header.Name,
                            Value = header.Value
                        }).ToList() ?? new List<Header>()
                    }).ToList() ?? new List<Response>()
                }).ToList() ?? new List<Method>()
            }).ToList() ?? new List<ApiResource>();

            var createdCarrier = await _carrierContext.Carriers.AddAsync(new Carrier
            {
                Name = carrier.Name,
                Nickname = carrier.Nickname,
                Icon = icon,
                Options = options,
                ApiResources = apiResources
            });
            await _carrierContext.SaveChangesAsync();

            return TypedResults.Ok(DTOMapper.MapCarrierToDTO(createdCarrier.Entity));
        }
        catch (DbUpdateException dbUpdateException)
        {
            if (dbUpdateException.InnerException.Message.Contains("Cannot insert duplicate key row in object"))
            {
                return TypedResults.Conflict(
                    $"The carrier configuration with name \'{carrier.Name}\' and nickname \'{carrier.Nickname}\' already exists.");
            }
        }
        catch (Exception ex)
        {
            return TypedResults.Problem(ex.ToString());
        }

        return TypedResults.BadRequest();
    }

    [HttpDelete]
    public async Task<IResult> DeleteCarrier(int carrierId = -1)
    {
        if (carrierId == -1)
        {
            return TypedResults.BadRequest("Carrier ID has to be provided");
        }
        
        try
        {
            var carrierToDelete = await _carrierContext.Carriers.FirstOrDefaultAsync(carrier => carrier.CarrierId == carrierId);

            if (carrierToDelete == null)
            {
                return TypedResults.NotFound($"Carrier configuration with ID: \'{carrierId}\' doesn't exist.");
            }

            _carrierContext.Carriers.Remove(carrierToDelete);
            await _carrierContext.SaveChangesAsync();

            return TypedResults.Ok($"Carrier with ID: \'{carrierId}\' has been deleted.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        return TypedResults.Ok();
    }
}