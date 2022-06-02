using Microsoft.AspNetCore.Mvc;
using MonitorMarkets.Application.Objects.Abstractions;
using MonitorMarkets.Application.Objects.DataBase;
using MonitorMarkets.Databases;

namespace MonitorMarket.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly LoggerContext _db;
    IRepository<LogInfo> dbLog;
    
    public HomeController(ILogger<HomeController> logger, IRepository<LogInfo> repositoryLog, IRepository<OrdersEntitiesInfo> repositoryOrder,
        IRepository<PositionsEntitiesInfo> repositoryPositions, IRepository<WalletEntitiesInfo> repositoryWallet)
    {
        _logger = logger;
        dbLog = repositoryLog;
    }

    #region Swagger

    #region Add

    /// <summary>
    /// Добавление логов
    /// </summary>
    /// <param name="logInfo"></param>
    /// <response code="200">Log добавлен в базу данных</response>
    /// <response code="400">неправильные параметры</response>
    [HttpPost]
    [Route("AddLog")] 
    [ProducesResponseType(typeof(LogInfo), 200)]
    public void AddLog(LogInfo logInfo)
    {
        dbLog.Create(logInfo);
    }
    #endregion

    #region Delete
    
    /// <summary>
    /// Удаление логов
    /// </summary>
    /// <param name="logInfo"></param>
    /// <response code="200">Log удален из базы данных</response>
    /// <response code="400">неправильные параметры</response>

    [HttpDelete]
    [Route("DelLog")]
    [ProducesResponseType(typeof(LogInfo), 200)]

    public void DelLog(LogInfo logInfo)
    {
        dbLog.Remove(logInfo);
    }
    #endregion

    #region Update

    /// <summary>
    /// Обновление логов
    /// </summary>
    /// <param name="logInfo"></param>
    /// <response code="200">Log обновлён</response>
    /// <response code="400">неправильные параметры</response>
    [HttpPost]
    [Route("UpLog")]
    [ProducesResponseType(typeof(LogInfo), 200)]

    public void UpLog(LogInfo logInfo)
    {
        dbLog.Update(logInfo);
    }
    #endregion

    #region FindById

    /// <summary>
    /// Поиск лога по id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="200">Log найден</response>
    /// <response code="400">неправильные параметры</response>

    [HttpGet]
    [Route("FLog")]
    [ProducesResponseType(typeof(LogInfo), 200)]

    public LogInfo FLog(string id)
    {
        var search = dbLog.FindById(id);
        return search;
    }
    #endregion
    
    #endregion
}