using System.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Mockingbird.API.Database;
using Mockingbird.API.DTO;

namespace Mockingbird.API.Controllers;

public class HeadersController: BaseController
{
    public HeadersController(CarrierContext carrierContext): base(carrierContext){}

    [HttpGet]
    public async Task<IResult> GetHeaders(Guid? responseId = null, int headerId = -1)
    {
        IQueryable<Header> query = _carrierContext.Headers.AsQueryable()
            .Include(header => header.ResponseId);

        if (responseId != null)
        {
            query = query.Where(header => header.ResponseId.Equals(responseId));
        }

        if (headerId > -1)
        {
            query = query.Where(header => header.HeaderId == headerId);
        }

        var result = query.ToList();

        if (result.Count == 0)
        {
            return TypedResults.NotFound("Headers not found");
        }

        return TypedResults.Ok(DTOMapper.MapHeadersToDTO(result));
    }

    [HttpPost]
    public async Task<IResult> PostHeaders(Guid responseId, HeaderDTO headerDto)
    {
        try
        {
            if (responseId == null)
            {
                return TypedResults.BadRequest("No response id provided in query");
            }
            
            if (headerDto == null)
            {
                return TypedResults.BadRequest();
            }

            var response = await _carrierContext.Responses
                .FirstOrDefaultAsync(response => response.ResponseId == responseId);

            if (response == null)
            {
                return TypedResults.NotFound(
                    $"The provided response with id: \'{response.ResponseId}\' couldn't be found.");
            }

            var header = new Header() 
            {
                Name = headerDto.Name,
                Value = headerDto.Value
            };
            
            response.Headers.Add(header);
            await _carrierContext.SaveChangesAsync();

            return TypedResults.Ok(DTOMapper.MapHeaderToDTO(header));
        }
        catch (Exception ex)
        {
            return TypedResults.Problem(ex.ToString());
        }
    }

    [HttpPut]
    public async Task<IResult> UpdateHeader(int headerId, HeaderDTO headerDto)
    {
        if (headerId == null || headerId <= 0)
        {
            return TypedResults.BadRequest("Provide a valid header id");
        }

        var header = await _carrierContext.Headers
            .FirstOrDefaultAsync(header => header.HeaderId == headerId);
        
        if (header == null)
        {
            return TypedResults.NotFound($"Header with id: \'{headerId}\' not found.");
        }

        header.Value = headerDto.Value;

        _carrierContext.Headers.Update(header);
        await _carrierContext.SaveChangesAsync();

        return TypedResults.Ok(DTOMapper.MapHeaderToDTO(header));
    }

    [HttpDelete]
    public async Task<IResult> DeleteHeader(int headerId = -1)
    {
        if (headerId == -1)
        {
            return TypedResults.BadRequest("Provide a valid header id");
        }

        var header = await _carrierContext.Headers.FirstOrDefaultAsync(header => header.HeaderId == headerId);
        if (header == null)
        {
            return TypedResults.NotFound($"Header with id: \'{headerId}\' not found.");
        }

        _carrierContext.Headers.Remove(header);
        await _carrierContext.SaveChangesAsync();

        return TypedResults.Ok();
    }
}