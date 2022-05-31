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
    
    [HttpPost]
    [Route("AddLog")]
    public void AddLog(LogInfo logInfo)
    {
        LogInfo ram = new LogInfo
        {
            TypeError = logInfo.TypeError,
            MsgError = logInfo.MsgError,
            Time = logInfo.Time,
        };
        dbLog.Create(ram);
    }
    
    
    [HttpPost]
    [Route("AddOrder")]
    public void AddOrder(OrdersEntitiesInfo orderInfo)
    {
        OrdersEntitiesInfo ram = new OrdersEntitiesInfo
        {
            Price = orderInfo.Price,
            Amount = orderInfo.Amount,
            Direction = orderInfo.Direction,
            StatusOrder = orderInfo.StatusOrder,
        };
        dbOrder.Create(ram);
    }
    
    
    [HttpPost]
    [Route("AddPositions")]
    public void AddPositions(PositionsEntitiesInfo posInfo)
    {
        PositionsEntitiesInfo ram = new PositionsEntitiesInfo
        {
            Symbol = posInfo.Symbol,
            Price = posInfo.Price,
            Amount = posInfo.Amount,
            StatusPosition = posInfo.StatusPosition,
        };
        dbPositions.Create(ram);
    }
   
    
    [HttpPost]
    [Route("AddWallet")]
    public void AddWallet(WalletEntitiesInfo walletInfo)
    {
        WalletEntitiesInfo ram = new WalletEntitiesInfo
        {
            Currency = walletInfo.Currency,
            Balance = walletInfo.Balance,
            Aviailable = walletInfo.Aviailable,
        };
        dbWallet.Create(ram);
    }

    #endregion

    #region Delete

    [HttpPost]
    [Route("DelLog")]
    public void DelLog(LogInfo logInfo)
    {
        LogInfo ram = new LogInfo
        {
            TypeError = logInfo.TypeError,
            MsgError = logInfo.MsgError,
            Time = logInfo.Time,
        };

        dbLog.Remove(ram);
    }

    [HttpPost]
    [Route("DelOrder")]
    public void DelOrder(OrdersEntitiesInfo orderInfo)
    {
        OrdersEntitiesInfo ram = new OrdersEntitiesInfo
        {
            Price = orderInfo.Price,
            Amount = orderInfo.Amount,
            Direction = orderInfo.Direction,
            StatusOrder = orderInfo.StatusOrder,
        };
        
        dbOrder.Remove(ram);
    }

    [HttpPost]
    [Route("DelPositions")]
    public void DelPositions(PositionsEntitiesInfo posInfo)
    {
        PositionsEntitiesInfo ram = new PositionsEntitiesInfo
        {
            Symbol = posInfo.Symbol,
            Price = posInfo.Price,
            Amount = posInfo.Amount,
            StatusPosition = posInfo.StatusPosition,
        };
        dbPositions.Remove(ram);
    }
    
    [HttpPost]
    [Route("DelWallet")]
    public void DelWallet(WalletEntitiesInfo walletInfo)
    {
        WalletEntitiesInfo ram = new WalletEntitiesInfo
        {
            Currency = walletInfo.Currency,
            Balance = walletInfo.Balance,
            Aviailable = walletInfo.Aviailable,
        };
        dbWallet.Remove(ram);
    }
    #endregion

    #region Update

    [HttpPost]
    [Route("UpLog")]
    public void UpLog(LogInfo logInfo)
    {
        LogInfo ram = new LogInfo
        {
            TypeError = logInfo.TypeError,
            MsgError = logInfo.MsgError,
            Time = logInfo.Time,
        };

        dbLog.Update(ram);
    }

    [HttpPost]
    [Route("UpOrder")]
    public void UpOrder(OrdersEntitiesInfo orderInfo)
    {
        OrdersEntitiesInfo ram = new OrdersEntitiesInfo
        {
            Price = orderInfo.Price,
            Amount = orderInfo.Amount,
            Direction = orderInfo.Direction,
            StatusOrder = orderInfo.StatusOrder,
        };
        
        dbOrder.Update(ram);
    }

    [HttpPost]
    [Route("UpPositions")]
    public void UpPositions(PositionsEntitiesInfo posInfo)
    {
        PositionsEntitiesInfo ram = new PositionsEntitiesInfo
        {
            Symbol = posInfo.Symbol,
            Price = posInfo.Price,
            Amount = posInfo.Amount,
            StatusPosition = posInfo.StatusPosition,
        };
        dbPositions.Update(ram);
    }
    
    [HttpPost]
    [Route("UpWallet")]
    public void UpWallet(WalletEntitiesInfo walletInfo)
    {
        WalletEntitiesInfo ram = new WalletEntitiesInfo
        {
            Currency = walletInfo.Currency,
            Balance = walletInfo.Balance,
            Aviailable = walletInfo.Aviailable,
        };
        dbWallet.Update(ram);
    }


    #endregion

    #region FindById

    [HttpPost]
    [Route("FLog")]
    public void FLog(int id)
    {
        dbLog.FindById(id);
    }

    [HttpPost]
    [Route("FOrder")]
    public void FOrder(int id)
    {
        dbOrder.FindById(id);
    }

    [HttpPost]
    [Route("FPositions")]
    public void FPositions(int id)
    {
        dbPositions.FindById(id);
    }
    
    [HttpPost]
    [Route("FWallet")]
    public void FWallet(int id)
    {
        dbWallet.FindById(id);
    }


    #endregion
    
    #endregion
}