using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using MonitorMarkets.Application.Objects.Abstractions;
using MonitorMarkets.Application.Objects.DataBase;
using MonitorMarkets.Databases;
using MonitorMarkets.Databases.Entities;
using MonitorMarkets.Application.Objects.Data.Enums;
using OrderActionEnum = MonitorMarkets.Application.Objects.DataBase.OrderActionEnum;
using StatusOrderEnum = MonitorMarkets.Application.Objects.DataBase.StatusOrderEnum;
using Swagger.Net.Annotations;

namespace MonitorMarket.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly LoggerContext _db;
    IRepository<LogInfo> dbLog;
    IRepository<OrdersEntitiesInfo> dbOrder;
    IRepository<PositionsEntitiesInfo> dbPositions;
    IRepository<WalletEntitiesInfo> dbWallet;
    
    public HomeController(ILogger<HomeController> logger, IRepository<LogInfo> repositoryLog, IRepository<OrdersEntitiesInfo> repositoryOrder,
        IRepository<PositionsEntitiesInfo> repositoryPositions, IRepository<WalletEntitiesInfo> repositoryWallet)
    {
        _logger = logger;
        
        dbLog = repositoryLog;
        dbOrder = repositoryOrder;
        dbPositions = repositoryPositions;
        dbWallet = repositoryWallet;
    }

    #region Swagger

    #region Add
    
    /// <summary>
    /// Добавление логов
    /// </summary>
    /// <param name="logInfo"></param>
    [HttpPost]
    [Route("AddLog")]
    [SwaggerResponse(HttpStatusCode.OK, "Log added in database")]
    [SwaggerResponse(HttpStatusCode.NotFound, "Error")]

    public void AddLog(LogInfo logInfo)
    {
        dbLog.Create(logInfo);
    }

    
    /// <summary>
    /// Добавление ордеров
    /// </summary>
    /// <param name="orderInfo"></param>
    [HttpPost]
    [Route("AddOrder")]
    public void AddOrder(OrdersEntitiesInfo orderInfo)
    {
        dbOrder.Create(orderInfo);
    }
    
    /// <summary>
    /// Добавление позиций
    /// </summary>
    /// <param name="posInfo"></param>
    [HttpPost]
    [Route("AddPositions")]
    public void AddPositions(PositionsEntitiesInfo posInfo)
    {
        dbPositions.Create(posInfo);
    }
   
    /// <summary>
    /// Добавление кошелька
    /// </summary>
    /// <param name="walletInfo"></param>
    [HttpPost]
    [Route("AddWallet")]
    public void AddWallet(WalletEntitiesInfo walletInfo)
    {
        dbWallet.Create(walletInfo);
    }

    #endregion

    #region Delete
    
    /// <summary>
    /// Удаление логов
    /// </summary>
    /// <param name="logInfo"></param>
    [HttpPost]
    [Route("DelLog")]
    public void DelLog(LogInfo logInfo)
    {
        dbLog.Remove(logInfo);
    }

    /// <summary>
    /// Удаление ордеров
    /// </summary>
    /// <param name="orderInfo"></param>
    [HttpPost]
    [Route("DelOrder")]
    public void DelOrder(OrdersEntitiesInfo orderInfo)
    {
        dbOrder.Remove(orderInfo);
    }

    /// <summary>
    /// Удаление позиций
    /// </summary>
    /// <param name="posInfo"></param>
    [HttpPost]
    [Route("DelPositions")]
    public void DelPositions(PositionsEntitiesInfo posInfo)
    {
        dbPositions.Remove(posInfo);
    }
    
    /// <summary>
    /// Удаление кошелька
    /// </summary>
    /// <param name="walletInfo"></param>
    [HttpPost]
    [Route("DelWallet")]
    public void DelWallet(WalletEntitiesInfo walletInfo)
    {
        dbWallet.Remove(walletInfo);
    }
    #endregion

    #region Update

    /// <summary>
    /// Обновление логов
    /// </summary>
    /// <param name="logInfo"></param>
    [HttpPost]
    [Route("UpLog")]
    public void UpLog(LogInfo logInfo)
    {
        dbLog.Update(logInfo);
    }

    /// <summary>
    /// Обновление ордеров
    /// </summary>
    /// <param name="orderInfo"></param>
    [HttpPost]
    [Route("UpOrder")]
    public void UpOrder(OrdersEntitiesInfo orderInfo)
    {
        dbOrder.Update(orderInfo);
    }

    /// <summary>
    /// Обновление позиций
    /// </summary>
    /// <param name="posInfo"></param>
    [HttpPost]
    [Route("UpPositions")]
    public void UpPositions(PositionsEntitiesInfo posInfo)
    {
        dbPositions.Update(posInfo);
    }
    
    /// <summary>
    /// Обновление кошелька
    /// </summary>
    /// <param name="walletInfo"></param>
    [HttpPost]
    [Route("UpWallet")]
    public void UpWallet(WalletEntitiesInfo walletInfo)
    {
        dbWallet.Update(walletInfo);
    }


    #endregion

    #region FindById

    /// <summary>
    /// Поиск лога по id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("FLog")]
    public LogInfo FLog(string id)
    {
        var search = dbLog.FindById(id);
        return search;
    }

    /// <summary>
    /// Поиск ордера по id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("FOrder")]
    public OrdersEntitiesInfo FOrder(string id)
    {
        var search = dbOrder.FindById(id);
        return search;
    }

    /// <summary>
    /// Поиск позиций по id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("FPositions")]
    public PositionsEntitiesInfo FPositions(string id)
    {
        var search = dbPositions.FindById(id);
        return search;
    }
    
    /// <summary>
    /// Поиск кошелька по id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("FWallet")]
    public WalletEntitiesInfo FWallet(string id)
    {
        var search = dbWallet.FindById(id);
        return search;
    }


    #endregion
    
    #endregion
}