using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mockingbird.API.Database;
using Mockingbird.API.DTO;

namespace Mockingbird.API.Controllers;

public class OptionsController : BaseController
{

    public OptionsController(CarrierContext carrierContext): base(carrierContext)
    {
    }

    [HttpGet]
    public async Task<IResult> GetOptions(int carrierId = -1, int optionId = -1)
    {
        IQueryable<Option> query = _carrierContext.Options.AsQueryable();

        if (carrierId > -1)
        {
            query = query.Where(option => option.CarrierId == carrierId);
        }

        if (optionId > -1)
        {
            query = query.Where(option => option.OptionId == optionId);
        }

        var result = await query.ToListAsync();

        if (result.Count == 0)
        {
            return TypedResults.NotFound("Option not found");
        }

        return TypedResults.Ok(DTOMapper.MapOptionsToDTO(result));
    }

    [HttpPost]
    public async Task<IResult> PostOption(int carrierId, OptionDTO option)
    {
        try
        {
            if (carrierId == null || carrierId == 0)
            {
                return TypedResults.BadRequest("Carrier ID missing in the query");
            }
            
            var carrier = await _carrierContext.Carriers.FirstOrDefaultAsync(carrier => carrier.CarrierId == carrierId);

            if (carrier == null)
            {
                return TypedResults.NotFound(
                    $"The provided carrier with id: \'{option.CarrierId}\' couldn't be found.");
            }

            carrier.Options ??= new List<Option>();

            var createdOption = new Option
            {
                Carrier = carrier,
                CarrierId = carrier.CarrierId,
                Name = option.Name,
                Value = option.Value
            };

            carrier.Options?.Add(createdOption);

            _carrierContext.Carriers.Update(carrier);
            await _carrierContext.SaveChangesAsync();

            return TypedResults.Ok(DTOMapper.MapOptionToDTO(createdOption));
        }
        catch (DbUpdateException dbUpdateException)
        {
            if (dbUpdateException.InnerException.Message.Contains("Cannot insert duplicate key row in object"))
            {
                return TypedResults.Conflict(
                    $"The option for carrier id: \'{carrierId}\' and provided name: \'{option.Name}\' and value: \'{option.Value}\' already exists.");
            }
        }
        catch (Exception ex)
        {
            return TypedResults.Problem(ex.ToString());
        }

        return TypedResults.Ok();
    }

    [HttpPut]
    public async Task<IResult> UpdateOption(int optionId, OptionDTO updateOption)
    {
        if (optionId == null)
        {
            return TypedResults.Problem("OptionId has to be provided!");
        }

        var option = await _carrierContext.Options.FirstOrDefaultAsync(option => option.OptionId == optionId);

        if (option == null)
        {
            return TypedResults.NotFound($"Option with the given id: {optionId} has not been found");
        }
        
        option.Value = updateOption.Value;

        _carrierContext.Options.Update(option);
        await _carrierContext.SaveChangesAsync();

        return TypedResults.Ok(DTOMapper.MapOptionToDTO(option));
    }
}