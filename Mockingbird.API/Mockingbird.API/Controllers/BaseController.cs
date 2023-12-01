using Microsoft.AspNetCore.Mvc;
using Mockingbird.API.Database;

namespace Mockingbird.API.Controllers;

[ApiController]
[Route("mockingbird/[controller]")]
public class BaseController: ControllerBase
{
    protected readonly CarrierContext _carrierContext;

    public BaseController(){}
    
    public BaseController(CarrierContext carrierContext)
    {
        _carrierContext = carrierContext;
    }
}