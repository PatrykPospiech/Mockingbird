using Microsoft.AspNetCore.Mvc;

namespace Mockingbird.API.Controllers;

public class RegressionTestController : ControllerBase
{
    [HttpGet("{carrierId}")]
    public async Task<IResult> GetRegressionTests(Guid carrierId)
    {
        return TypedResults.Ok();
    }
}