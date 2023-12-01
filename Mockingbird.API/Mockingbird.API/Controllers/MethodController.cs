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

        if (apiResourceId > 0)
        {
            query = query.Where(method => method.ApiResourceId.Equals(apiResourceId));
        }

        if (methodId > 0)
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
    public async Task<IResult> PostMethod(int apiResourceId, MethodDTO method)
    {
        try
        {
            if (apiResourceId == null || apiResourceId < 0)
            {
                return TypedResults.BadRequest("No api resource id provided in query");
            }
            
            if (method == null)
            {
                return TypedResults.BadRequest();
            }

            var resource = await _carrierContext.ApiResources
                .Include(resource => resource.Methods)
                .ThenInclude(method => method.Responses)
                .ThenInclude(response => response.Headers)
                .FirstOrDefaultAsync(response => response.ApiResourceId == apiResourceId);

            if (resource == null)
            {
                return TypedResults.NotFound(
                    $"The provided api resource with id: \'{apiResourceId}\' couldn't be found.");
            }

            var createdMethod = new Method
            {
                Name = method.Name,
                MethodType = method.MethodType,
                Responses = method.Responses?.Select(response => new Response()
                {
                    IsActive = response.IsActive ?? false,
                    ResponseStatusCode = response.ResponseStatusCode,
                    ResponseBody = response.ResponseBody,
                    Headers = response.Headers?.Select(header => new Header
                    {
                        Name = header.Name,
                        Value = header.Value
                    }).ToList() ?? new List<Header>()
                }).ToList() ?? new List<Response>()
            };
            
            resource.Methods.Add(createdMethod);
            
            _carrierContext.ApiResources.Update(resource);
            await _carrierContext.SaveChangesAsync();

            return TypedResults.Ok(DTOMapper.MapMethodToDTO(createdMethod));
        }
        catch (Exception ex)
        {
            return TypedResults.Problem(ex.ToString());
        }
    }
    
    [HttpPut]
    public async Task<IResult> UpdateMethod(int methodId, MethodDTO methodDto)
    {
        if (methodId == null || methodId < 0)
        {
            return TypedResults.BadRequest("Method id not provided.");
        }

        if (methodDto == null)
        {
            return TypedResults.BadRequest();
        }

        var method = await _carrierContext.Method
            .Include(method => method.Responses)
            .ThenInclude(response => response.Headers)
            .FirstOrDefaultAsync(method => method.MethodId == methodId);

        method.MethodType = methodDto.MethodType;

        _carrierContext.Method.Update(method);
        await _carrierContext.SaveChangesAsync();

        return TypedResults.Ok(DTOMapper.MapMethodToDTO(method));
    }
    
    [HttpDelete]
    public async Task<IResult> DeleteMethod(int methodId = -1)
    {
        if (methodId == null || methodId < 0)
        {
            return TypedResults.BadRequest("Method id not provided.");
        }

        var methodToDelete = await _carrierContext.Method.FirstOrDefaultAsync(method => method.MethodId == methodId);

        _carrierContext.Method.Remove(methodToDelete);
        await _carrierContext.SaveChangesAsync();

        return TypedResults.Ok();
    }
}