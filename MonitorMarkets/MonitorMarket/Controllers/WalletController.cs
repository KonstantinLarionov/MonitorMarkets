﻿using Microsoft.AspNetCore.Mvc;
using MonitorMarkets.Application.Objects.Abstractions;
using MonitorMarkets.Application.Objects.DataBase;
using MonitorMarkets.Databases;

namespace MonitorMarket.Controllers;


[Route("walletController")]
public class WalletController : Controller
{
    private readonly ILogger<WalletController> _logger;
    private readonly LoggerContext _db;
    IRepository<WalletEntitiesInfo> dbWallet;

    public WalletController(ILogger<WalletController> logger,
         IRepository<WalletEntitiesInfo> repositoryWallet)
    {
        _logger = logger;
        dbWallet = repositoryWallet;
    }

    #region Swagger

    #region Add

    /// <summary>
    /// Добавление кошелька
    /// </summary>
    /// <param name="walletInfo"></param>
    /// <response code="200">Wallet добавлен в базу данных</response>
    /// <response code="400">неправильные параметры</response>

    [HttpPost]
    [Route("wallet/addwallet")]
    [ProducesResponseType(typeof(WalletEntitiesInfo), 200)]

    public IActionResult AddWallet([FromBody]WalletEntitiesInfo walletInfo)
    {
        var result = dbWallet.Create(walletInfo);
        if (result == 0)
        {
            return BadRequest();
        }
        else
        {
            return Ok();
        }
    }


    #endregion

    #region Delete

    /// <summary>
    /// Удаление кошелька
    /// </summary>
    /// <param name="walletInfo"></param>
    /// <response code="200">Wallet удален из базы данных</response>
    /// <response code="400">неправильные параметры</response>

    [HttpDelete]
    [Route("wallet/deletewallet")]
    [ProducesResponseType(typeof(WalletEntitiesInfo), 200)]

    public IActionResult DelWallet([FromQuery]Guid id)
    {
        var result = dbWallet.Remove(id);
        if (result == 0)
        {
            return BadRequest();
        }
        else
        {
            return Ok(id);
        }

    }


    #endregion

    #region Update

    /// <summary>
    /// Обновление кошелька
    /// </summary>
    /// <param name="walletInfo"></param>
    /// <response code="200">Wallet обновлён</response>
    /// <response code="400">неправильные параметры</response>

    [HttpPut]
    [Route("wallet/updatewallet")]
    [ProducesResponseType(typeof(WalletEntitiesInfo), 200)]

    public IActionResult UpWallet([FromQuery]Guid id, [FromBody]WalletEntitiesInfo walletInfo)
    {
        var result = dbWallet.Update(walletInfo, id);
        if (result == 0)
        {
            return BadRequest();
        }
        else
        {
            return Ok(id);
        }

    }


    #endregion

    #region FindById

    /// <summary>
    /// Поиск кошелька по id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="200">Wallet найден</response>
    /// <response code="400">неправильные параметры</response>

    [HttpGet]
    [Route("wallet/findwallet")]
    [ProducesResponseType(typeof(WalletEntitiesInfo), 200)]

    public WalletEntitiesInfo FWallet([FromQuery]Guid id)
    {
        var search = dbWallet.FindById(id);
        return search;
    }


    #endregion
    
    #endregion
    
}
