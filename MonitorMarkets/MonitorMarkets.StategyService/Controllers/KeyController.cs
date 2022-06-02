using Microsoft.AspNetCore.Mvc;
using MonitorMarkets.Application.Objects.Abstractions;
using MonitorMarkets.DesktopDatabase.Entities;
using MonitorMarkets.DesktopDatabase.Repositories;
using MonitorMarkets.StategyService.Requests;
using MonitorMarkets.StategyService.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace MonitorMarkets.StategyService.Controllers;

public class KeyController:Controller

{
    private readonly IRepository<ConnectionKeys> _keyRepository;
    
    public KeyController(IRepository<ConnectionKeys> keyRepository)
    {
        _keyRepository = keyRepository;
    }
    
    [HttpPost]
    [Route("CreateKey")]
    [SwaggerResponse(StatusCodes.Status200OK, "Получение бизнес процесса", typeof(CreateKeyRequest))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Получение бизнес процесса", typeof(CreateKeyResponse))]
    public async Task <IActionResult> CreateKey([FromQuery]CreateKeyRequest request)
    {
        var Keys = new ConnectionKeys()
            { PassPhrase = request.PassPhrase, PublicKeys = request.PublicKey, SecretKey = request.SecretKey };
        var res = _keyRepository.Create(Keys);
        if (true)
            return Ok(new CreateKeyResponse() { Sucsess = true });
        else
            return BadRequest(new CreateKeyResponse() { Sucsess = false, Message = "cannot create" });

    }
    

}