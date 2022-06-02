using Microsoft.AspNetCore.Mvc;
using MonitorMarkets.Application.Objects.Abstractions;
using MonitorMarkets.Application.Objects.DataBase;
using MonitorMarkets.Databases;

namespace MonitorMarket.Controllers;

public class OrderController : Controller
{
    private readonly ILogger<OrderController> _logger;
    private readonly LoggerContext _db;
    IRepository<OrdersEntitiesInfo> dbOrder;
    
    public OrderController(ILogger<OrderController> logger, IRepository<LogInfo> repositoryLog, IRepository<OrdersEntitiesInfo> repositoryOrder,
        IRepository<PositionsEntitiesInfo> repositoryPositions, IRepository<WalletEntitiesInfo> repositoryWallet)
    {
        _logger = logger;
        dbOrder = repositoryOrder;
    }

    #region Swagger

    #region Add
    /// <summary>
    /// Добавление ордеров
    /// </summary>
    /// <param name="orderInfo"></param>
    /// <response code="200">Order добавлен в базу данных</response>
    /// <response code="400">неправильные параметры</response>

    [HttpPut]
    [Route("order/addorder")]
    [ProducesResponseType(typeof(OrdersEntitiesInfo), 200)]

    public void AddOrder(OrdersEntitiesInfo orderInfo)
    {
        dbOrder.Create(orderInfo);
    }
    #endregion
    
    #region Delete
    /// <summary>
    /// Удаление ордеров
    /// </summary>
    /// <param name="orderInfo"></param>
    /// <response code="200">Order удален из базы данных</response>
    /// <response code="400">неправильные параметры</response>

    [HttpDelete]
    [Route("order/deleteorder")]
    [ProducesResponseType(typeof(OrdersEntitiesInfo), 200)]

    public void DelOrder(OrdersEntitiesInfo orderInfo)
    {
        dbOrder.Remove(orderInfo);
    }
    #endregion
    
    #region Update
    /// <summary>
    /// Обновление ордеров
    /// </summary>
    /// <param name="orderInfo"></param>
    /// <response code="200">Order обновлён</response>
    /// <response code="400">неправильные параметры</response>

    [HttpPost]
    [Route("order/updateorder")]
    [ProducesResponseType(typeof(OrdersEntitiesInfo), 200)]

    public void UpOrder(OrdersEntitiesInfo orderInfo)
    {
        dbOrder.Update(orderInfo);
    }
    #endregion

    #region FindById
    /// <summary>
    /// Поиск ордера по id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="200">Order найден</response>
    /// <response code="400">неправильные параметры</response>

    [HttpGet]
    [Route("order/findorder")]
    [ProducesResponseType(typeof(OrdersEntitiesInfo), 200)]

    public OrdersEntitiesInfo FOrder(string id)
    {
        var search = dbOrder.FindById(id);
        return search;
    }
    #endregion

    #endregion
    
}