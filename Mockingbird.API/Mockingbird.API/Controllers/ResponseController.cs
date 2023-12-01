using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mockingbird.API.Database;
using Mockingbird.API.DTO;

namespace Mockingbird.API.Controllers;

public class ResponsesController : BaseController
{
    public ResponsesController(CarrierContext carrierContext) : base(carrierContext){}

    [HttpGet]
    public async Task<IResult> GetResponses(int methodId, Guid? responseId)
    {
        IQueryable<Response> query = _carrierContext.Responses.AsQueryable()
            .Include(response => response.Headers);

        if (methodId > 0)
        {
            query = query.Where(response => response.MethodId == methodId);
        }

        if (responseId != null)
        {
            query = query.Where(response => response.ResponseId.Equals(responseId));
        }

        var result = await query.ToListAsync();

        if (result.Count == 0)
        {
            return TypedResults.NotFound("No responses found");
        }

        return TypedResults.Ok(DTOMapper.MapResponsesToDTO(result));
    }

    [HttpPost]
    public async Task<IResult> PostResponse(int methodId, ResponseDTO responseDto)
    {
        if (methodId == 0)
        {
            return TypedResults.BadRequest("Method id not provided");
        }

        if (responseDto == null)
        {
            return TypedResults.BadRequest();
        }

        var method = await _carrierContext.Method
            .Include(method => method.Responses)
            .ThenInclude(response => response.Headers)
            .FirstOrDefaultAsync(method => method.MethodId == methodId);

        if (method == null)
        {
            return TypedResults.NotFound("Method with provided methodId doesn't exist.");
        }

        var response = new Response
        {
            ResponseStatusCode = responseDto.ResponseStatusCode,
            ResponseBody = responseDto.ResponseBody,
            Headers = responseDto.Headers?.Select(header => new Header
            {
                Name = header.Name,
                Value = header.Value
            }).ToList() ?? new List<Header>()
        };
        
        method.Responses.Add(response);
        _carrierContext.Method.Update(method);
        await _carrierContext.SaveChangesAsync();

        return TypedResults.Ok(DTOMapper.MapResponseToDTO(response));
    }

    [HttpPut]
    public async Task<IResult> UpdateResponse(Guid? responseId, ResponseDTO responseDto)
    {
        if (responseId == null)
        {
            return TypedResults.BadRequest("Response id has to be provided.");
        }

        var response = await _carrierContext.Responses.FirstOrDefaultAsync(response => response.ResponseId.Equals(responseId));

        if (response == null)
        {
            return TypedResults.NotFound($"Response with id: \'{responseId}\'");
        }

        response.IsActive = responseDto.IsActive ?? false;
        response.ResponseBody = responseDto.ResponseBody ?? response.ResponseBody;
        response.ResponseStatusCode = responseDto.ResponseStatusCode ?? response.ResponseStatusCode;

        _carrierContext.Responses.Update(response);
        await _carrierContext.SaveChangesAsync();

        return TypedResults.Ok(DTOMapper.MapResponseToDTO(response));
    }

    [HttpDelete]
    public async Task<IResult> DeleteResponse(Guid? responseId)
    {
        if (responseId == null)
        {
            return TypedResults.BadRequest("Response id has to be provided.");
        }

        var response =
            await _carrierContext.Responses.FirstOrDefaultAsync(response => response.ResponseId.Equals(responseId));

        if (response == null)
        {
            return TypedResults.NotFound($"The response with id: \'{responseId}\' doesn't exist.");
        }

        _carrierContext.Responses.Remove(response);
        _carrierContext.SaveChangesAsync();

        return TypedResults.Ok();
    }
}