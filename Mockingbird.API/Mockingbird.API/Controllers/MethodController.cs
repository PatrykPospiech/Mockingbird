using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mockingbird.API.Database;
using Mockingbird.API.DTO;

namespace Mockingbird.API.Controllers;

public class MethodsController: BaseController
{
    public MethodsController(CarrierContext carrierContext) : base(carrierContext)
    {
    }

    [HttpGet]
    public async Task<IResult> GetMethod(int apiResourceId = -1, int methodId = -1)
    {
        IQueryable<Method> query = _carrierContext.Method.AsQueryable()
            .Include(method => method.Responses)
            .ThenInclude(response => response.Headers);

        if (apiResourceId != null)
        {
            query = query.Where(method => method.ApiResourceId.Equals(apiResourceId));
        }

        if (methodId > -1)
        {
            query = query.Where(method => method.MethodId == methodId);
        }

        var result = await query.ToListAsync();

        if (result.Count == 0)
        {
            return TypedResults.NotFound("Methods not found");
        }

        return TypedResults.Ok(DTOMapper.MapMethodsToDTO(result));
    }
    
    [HttpPost]
    public async Task<IResult> PostMethod()
    {
        throw new NotImplementedException();
    }
    
    [HttpPut]
    public async Task<IResult> UpdateMethod()
    {
        throw new NotImplementedException();
    }
    
    [HttpDelete]
    public async Task<IResult> DeleteMethod()
    {
        throw new NotImplementedException();
    }
}