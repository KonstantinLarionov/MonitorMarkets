using Microsoft.AspNetCore.Mvc;
using MonitorMarkets.Application.Objects.Abstractions;
using MonitorMarkets.Application.Objects.DataBase;
using MonitorMarkets.Databases;

namespace MonitorMarket.Controllers;

public class PositionsController : Controller
{
    private readonly ILogger<PositionsController> _logger;
    private readonly LoggerContext _db;
    IRepository<PositionsEntitiesInfo> dbPositions;

    public PositionsController(ILogger<PositionsController> logger, IRepository<LogInfo> repositoryLog, IRepository<OrdersEntitiesInfo> repositoryOrder,
        IRepository<PositionsEntitiesInfo> repositoryPositions, IRepository<WalletEntitiesInfo> repositoryWallet)
    {
        _logger = logger;
        dbPositions = repositoryPositions;
    }

    #region Swagger

    #region Add

    /// <summary>
    /// Добавление позиций
    /// </summary>
    /// <param name="posInfo"></param>
    /// <response code="200">Positions добавлен в базу данных</response>
    /// <response code="400">неправильные параметры</response>

    [HttpPut]
    [Route("positions/addpositions")]
    [ProducesResponseType(typeof(PositionsEntitiesInfo), 200)]

    public void AddPositions(PositionsEntitiesInfo posInfo)
    {
        dbPositions.Create(posInfo);
    }
    
    #endregion

    #region Delete

    /// <summary>
    /// Удаление позиций
    /// </summary>
    /// <param name="posInfo"></param>
    /// <response code="200">Positions удален из базы данных</response>
    /// <response code="400">неправильные параметры</response>

    [HttpDelete]
    [Route("positions/deletepositions")]
    [ProducesResponseType(typeof(PositionsEntitiesInfo), 200)]

    public void DelPositions(PositionsEntitiesInfo posInfo)
    {
        dbPositions.Remove(posInfo);
    }
    
    #endregion

    #region Update

    /// <summary>
    /// Обновление позиций
    /// </summary>
    /// <param name="posInfo"></param>
    /// <response code="200">Positions обновлён</response>
    /// <response code="400">неправильные параметры</response>

    [HttpPost]
    [Route("positions/updatepositions")]
    [ProducesResponseType(typeof(PositionsEntitiesInfo), 200)]

    public void UpPositions(PositionsEntitiesInfo posInfo)
    {
        dbPositions.Update(posInfo);
    }

    #endregion

    #region FindById

    /// <summary>
    /// Поиск позиций по id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="200">Positions найден</response>
    /// <response code="400">неправильные параметры</response>

    [HttpGet]
    [Route("positions/findpositions")]
    [ProducesResponseType(typeof(PositionsEntitiesInfo), 200)]

    public PositionsEntitiesInfo FPositions(string id)
    {
        var search = dbPositions.FindById(id);
        return search;
    }


    #endregion
    
    #endregion
    
}