using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mockingbird.API.Database;

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
    
    [HttpGet("/{id}")]
    public async Task<IResult> GetCarrier(int id)
    {
        var carrier = await _carrierContext.Carriers.FindAsync(id);

        if (carrier == null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok();
    }
    
    [HttpGet]
    public async Task<IResult> GetCarriers()
    {
        var carriers = await _carrierContext.Carriers
            .Select(carrier => new
            {
                Id = carrier.CarrierId,
                Name = carrier.Name,
                Nickname = carrier.Nickname,
                Icon = carrier.Icon
            })
            .ToListAsync();

        return TypedResults.Ok(carriers);
    }

    [HttpPost]
    public async Task<IResult> PostCarrier(Carrier carrier)
    {
        try
        {
            if (carrier == null)
            {
                return TypedResults.BadRequest();
            }

            var createdCarrier = _carrierContext.Carriers.Add(carrier);
            await _carrierContext.SaveChangesAsync();

            return TypedResults.Ok();
        }
        catch (DbUpdateException dbUpdateException)
        {
            if (dbUpdateException.InnerException.Message.Contains("Cannot insert duplicate key row in object"))
            {
                return TypedResults.Conflict(
                    $"The carrier configuration with name \'{carrier.Name}\' and nickname \'{carrier.Nickname}\' already exists");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        return TypedResults.BadRequest();
    }
}