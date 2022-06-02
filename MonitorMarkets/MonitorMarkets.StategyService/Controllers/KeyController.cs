using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            { PassPhrase = request.PassPhrase, PublicKeys = request.PublicKey, SecretKeys = request.SecretKey };
        var res = _keyRepository.Create(Keys);
        if (res != 0)
            return Ok(new CreateKeyResponse() { Sucsess = true });
        else
            return BadRequest(new CreateKeyResponse() { Sucsess = false, Message = "cannot create" });

    }

    public async Task<IActionResult> GetKey([FromHeader] GetKeyResponse request)
    {
        var res = _keyRepository.FindById(new Guid(""));
        if (true)
            return Ok(new GetKeyRequest());
        else
            return BadRequest(new GetKeyRequest() );
    }
    
    public async Task<IActionResult> PutKey([FromBody] PutKeyRequest request )
    {
        var item = _keyRepository.FindById(new Guid());
        item.PassPhrase = request.PassPhrase;
        item.PublicKeys = request.PublicKey;
        item.SecretKeys = request.SecretKey;
        var res = _keyRepository.Update(item ,new Guid(""));
        if (true)
            return Ok(new PutKeyResponse() { Sucsess = true });
        else
            return BadRequest(new PutKeyResponse() { Sucsess = false, Message = "cannot update" });
    }

}