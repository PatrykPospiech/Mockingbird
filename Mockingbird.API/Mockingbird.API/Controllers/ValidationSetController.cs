using Microsoft.AspNetCore.Mvc;
using Mockingbird.API.Database;
using Mockingbird.API.Logic;

namespace Mockingbird.API.Controllers;

public class ValidationSetController : BaseController
{

    public ValidationSetController() { }

    public async Task<IActionResult> GetRequestForCarrier(int carrierId)
    {
        var communication = _carrierContext.Carriers.FirstOrDefault(i => i.CarrierId == carrierId).TpsCommunications;

        if(communication == null)
        {
            return NotFound($"Carrier with ID: \'{carrierId}\' does not exist.");
        }
        
        var requestFilePath = IOFilesHelper.DecodeBase64ToFile(communication.ToList().First().RequestBase64);
        
        return PhysicalFile(requestFilePath, "application/json", string.Concat(DateTime.Now.ToString("yyyyMMddHHmmss"), ".json"));
    }
    
    [HttpGet("{carrierId}")]
    public async Task<IActionResult> GetValidationSets(int carrierId)
    {
        var communication = _carrierContext.Carriers.FirstOrDefault(i => i.CarrierId == carrierId).TpsCommunications;

        if(communication == null)
        {
            return NotFound($"Carrier with ID: \'{carrierId}\' does not exist.");
        }

        var requests = communication.Where(i => i.RequestBase64 != null).Select(i => i.RequestBase64);
        var responses = communication.Where(i => i.ResponseBase64 != null).Select(i => i.ResponseBase64);
        
        var zipFilePath = IOFilesHelper.CombineZipFileForCarrierOutputs(requests.ToArray(), responses.ToArray());
        return PhysicalFile(zipFilePath, "application/zip", DateTime.Now.ToString("yyyyMMddHHmmss")+".zip");
    }
}