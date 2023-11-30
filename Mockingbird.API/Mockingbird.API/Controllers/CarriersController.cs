using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mockingbird.API.Database;
using Mockingbird.API.DTO;

namespace Mockingbird.API.Controllers;

[ApiController]
[Route("mockingbird/[controller]")]
public class CarriersController : ControllerBase
{
    private CarrierContext _carrierContext;

    public CarriersController(CarrierContext carrierContext)
    {
        _carrierContext = carrierContext;
    }

    [HttpGet]
    public async Task<IResult> GetCarriers(int carrierId = -1)
    {
        if (carrierId > -1)
        {
            var carrier = _carrierContext.Carriers.FirstOrDefault(carrier => carrier.CarrierId == carrierId);

            if (carrier == null)
            {
                return TypedResults.NotFound($"Carrier with ID: \'{carrierId}\' does not exist.");
            }
            
            return TypedResults.Ok(carrier);
        }

        var carriers = await _carrierContext.Carriers
            .Select(carrier => new CarrierOutputDTO()
            {
                CarrierId = carrier.CarrierId,
                Name = carrier.Name,
                Nickname = carrier.Nickname,
                Icon = carrier.Icon,
                Options = carrier.Options,
                ApiResources = carrier.ApiResources,
            })
            .ToListAsync();

        
        return TypedResults.Ok(carriers);
    }

    [HttpPost]
    public async Task<IResult> PostCarrier(CarrierInputDTO carrier)
    {
        try
        {
            if (carrier == null)
            {
                return TypedResults.BadRequest();
            }

            var createdCarrier = await _carrierContext.Carriers.AddAsync(new Carrier
            {
                Name = carrier.Name,
                Nickname = carrier.Nickname,
                Icon = carrier.Icon
            });
            await _carrierContext.SaveChangesAsync();

            return TypedResults.Ok(new CarrierOutputDTO
            {
                CarrierId = createdCarrier.Entity.CarrierId,
                Name = createdCarrier.Entity.Name,
                Nickname = createdCarrier.Entity.Nickname,
                Icon = createdCarrier.Entity.Icon,
                Options = createdCarrier.Entity.Options,
                ApiResources = createdCarrier.Entity.ApiResources
            });
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
            var carrierToDelete = _carrierContext.Carriers.FirstOrDefault(carrier => carrier.CarrierId == carrierId);

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