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
    [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(CreateKeyResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Fail", typeof(CreateKeyResponse))]
    public async Task <IActionResult> CreateKey([FromBody]CreateKeyRequest request)
    {
        var Keys = new ConnectionKeys()
            { PassPhrase = request.PassPhrase, PublicKeys = request.PublicKey, SecretKeys = request.SecretKey, MarketsEnum = request.MarketsEnum};
        var res = _keyRepository.Create(Keys);
        if (res != 0)
            return Ok(new CreateKeyResponse() { Sucsess = true });
        else
            return BadRequest(new CreateKeyResponse() { Sucsess = false, Message = "cannot create new key" });

    }
    [HttpGet]
    [Route("GetKey")]
    [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(GetKeyResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Fail", typeof(GetKeyResponse))]
    public async Task<IActionResult> GetKey([FromQuery] GetKeyRequest request)
    {
        var res = _keyRepository.FindById(new Guid("08da4540-8704-45b0-8a16-edada53a699c"));
        if (res != null )
            return Ok(res);
        else
            return BadRequest(new GetKeyResponse() {Sucsess = false, Message = "cannot get keys"});
    }
    [HttpPut]
    [Route("UpdateKey")]
    [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(UpdateKeyResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Fail", typeof(UpdateKeyResponse))]
    public async Task<IActionResult> UpdateKey([FromBody] UpdateKeyRequest request )
    {
        var item = _keyRepository.FindById(new Guid("08da4540-8704-45b0-8a16-edada53a699c"));
        item.PassPhrase = request.PassPhrase;
        item.PublicKeys = request.PublicKey;
        item.SecretKeys = request.SecretKey;
        item.MarketsEnum = request.MarketsEnum;
        var res = _keyRepository.Update(item ,new Guid("08da4540-8704-45b0-8a16-edada53a699c"));
        if (res!=0)
            return Ok(new UpdateKeyResponse() { Sucsess = true });
        else
            return BadRequest(new UpdateKeyResponse() { Sucsess = false, Message = "cannot update values in secretKey, publicKey, passPhrase" });
    }

}