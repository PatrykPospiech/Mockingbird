using Microsoft.AspNetCore.Mvc;
using Mockingbird.API.Logic;

namespace Mockingbird.API.Controllers;

public class ValidationSetController : BaseController
{
    [HttpGet("{carrierId}")]
    public async Task<IActionResult> GetValidationSets(Guid carrierId)
    {
        var communication = TpsCommunications.FirstOrDefault(i => i.CarrierId == carrierId);
        
        if(communication == null)
        {
            return NotFound($"Carrier with ID: \'{carrierId}\' does not exist.");
        }
        
        var filePath = IOFilesHelper.DecodeBase64ToFile(communication.RequestBase64);
        
        return PhysicalFile(filePath, "application/json", string.Concat(DateTime.Now.ToString("yyyyMMddHHmmss"), ".json"));
    }

//TODO: zapiac to w bazie danych
    public List<TPSCommunication> TpsCommunications = new List<TPSCommunication>()
    {
        new TPSCommunication()
        {
            Id = 1,
            CarrierId = Guid.Parse("b3f9c9a0-0b1a-4e1a-8b0a-0b9b6f9b9b9b"),
            RequestBase64 = "dGVzdA==",
            ResponseBase64 = "RRdGVzdA=="
        }
    };

    public class TPSCommunication
    {
        public int Id { get; set; }
        public Guid CarrierId { get; set; }
        public string RequestBase64 { get; set; }
        public string ResponseBase64 { get; set; }
    }
    
}