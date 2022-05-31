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

    #region Post
    
    [HttpPost]
    [Route("AddLog")]
    public void AddLog(string typeError, string msgError)
    {
        LogInfo ram = new LogInfo
        {
            TypeError = typeError,
            MsgError = msgError,
            Time = DateTime.Now,
        };
        dbLog.Create(ram);
    }
    
    
    [HttpPost]
    [Route("AddOrder")]
    public void AddOrder(decimal price, decimal amount)
    {
        OrdersEntitiesInfo ram = new OrdersEntitiesInfo
        {
            Price = price,
            Amount = amount,
            Direction = OrderActionEnum.Buy,
            StatusOrder = StatusOrderEnum.Accepted,
        };
        dbOrder.Create(ram);
    }
    
    
    [HttpPost]
    [Route("AddPositions")]
    public void AddPositions(string symbol, decimal price, decimal amount)
    {
        PositionsEntitiesInfo ram = new PositionsEntitiesInfo
        {
            Symbol = symbol,
            Price = price,
            Amount = amount,
            StatusPosition = OrderActionEnum.Buy,
        };
        dbPositions.Create(ram);
    }
   
    
    [HttpPost]
    [Route("AddWallet")]
    public void AddWallet(string currency, decimal balance, decimal aviailable)
    {
        WalletEntitiesInfo ram = new WalletEntitiesInfo
        {
            Currency = currency,
            Balance = balance,
            Aviailable = aviailable,
        };
        dbWallet.Create(ram);
    }

    #endregion

    #region Delete

    [HttpPost]
    [Route("DelLog")]
    public void DelLog(string typeError, string msgError)
    {
        LogInfo ram = new LogInfo
        {
            TypeError = typeError,
            MsgError = msgError,
            Time = DateTime.Now,
        };

        dbLog.Remove(ram);
    }

    [HttpPost]
    [Route("DelOrder")]
    public void DelOrder(decimal price, decimal amount)
    {
        OrdersEntitiesInfo ram = new OrdersEntitiesInfo
        {
            Price = price,
            Amount = amount,
            Direction = OrderActionEnum.Buy,
            StatusOrder = StatusOrderEnum.Accepted,
        };
        
        dbOrder.Remove(ram);
    }

    [HttpPost]
    [Route("DelPositions")]
    public void DelPositions(string symbol, decimal price, decimal amount)
    {
        PositionsEntitiesInfo ram = new PositionsEntitiesInfo
        {
            Symbol = symbol,
            Price = price,
            Amount = amount,
            StatusPosition = OrderActionEnum.Buy,
        };
        dbPositions.Remove(ram);
    }
    
    [HttpPost]
    [Route("DelWallet")]
    public void DelWallet(string currency, decimal balance, decimal aviailable)
    {
        WalletEntitiesInfo ram = new WalletEntitiesInfo
        {
            Currency = currency,
            Balance = balance,
            Aviailable = aviailable,
        };
        dbWallet.Remove(ram);
    }
    #endregion

    #region Update

    [HttpPost]
    [Route("UpLog")]
    public void UpLog(string typeError, string msgError)
    {
        LogInfo ram = new LogInfo
        {
            TypeError = typeError,
            MsgError = msgError,
            Time = DateTime.Now,
        };

        dbLog.Update(ram);
    }

    [HttpPost]
    [Route("UpOrder")]
    public void UpOrder(decimal price, decimal amount)
    {
        OrdersEntitiesInfo ram = new OrdersEntitiesInfo
        {
            Price = price,
            Amount = amount,
            Direction = OrderActionEnum.Buy,
            StatusOrder = StatusOrderEnum.Accepted,
        };
        
        dbOrder.Update(ram);
    }

    [HttpPost]
    [Route("UpPositions")]
    public void UpPositions(string symbol, decimal price, decimal amount)
    {
        PositionsEntitiesInfo ram = new PositionsEntitiesInfo
        {
            Symbol = symbol,
            Price = price,
            Amount = amount,
            StatusPosition = OrderActionEnum.Buy,
        };
        dbPositions.Update(ram);
    }
    
    [HttpPost]
    [Route("UpWallet")]
    public void UpWallet(string currency, decimal balance, decimal aviailable)
    {
        WalletEntitiesInfo ram = new WalletEntitiesInfo
        {
            Currency = currency,
            Balance = balance,
            Aviailable = aviailable,
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