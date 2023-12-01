using Microsoft.AspNetCore.Mvc;
using Mockingbird.API.Database;
using Mockingbird.API.Logic;

namespace Mockingbird.API.Controllers;

public class ValidationSetController : BaseController
{

    public ValidationSetController() { }

    public async Task<IActionResult> GetRequestForCarrier(Guid carrierId)
    {
        var communication = _carrierContext.Carriers.FirstOrDefault(i => i.CarrierId == carrierId).TpsCommunications;

        if(communication == null)
        {
            return NotFound($"Carrier with ID: \'{carrierId}\' does not exist.");
        }
        
        var requestFilePath = IOFilesHelper.DecodeBase64ToFile(communication.RequestBase64);
        
        return PhysicalFile(filePath, "application/json", string.Concat(DateTime.Now.ToString("yyyyMMddHHmmss"), ".json"));
    }
    
    [HttpGet("{carrierId}")]
    public async Task<IActionResult> GetValidationSets(Guid carrierId)
    {
        var communication = _carrierContext.Carriers.FirstOrDefault(i => i.CarrierId == carrierId).TpsCommunications;

        if(communication == null)
        {
            return NotFound($"Carrier with ID: \'{carrierId}\' does not exist.");
        }

        var requests = communication.Select(i => i.RequestBase64);
        var responses = communication.Select(i => i.ResponseBase64);
        
        var zipFilePath = IOFilesHelper.CombineZipFileForCarrierOutputs(requests, responses);
        return PhysicalFile(zipFilePath, "application/zip", DateTime.Now.ToString("yyyyMMddHHmmss")+".zip");
    }
}