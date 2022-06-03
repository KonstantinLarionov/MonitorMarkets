using Microsoft.AspNetCore.Mvc;
using MonitorMarkets.Application.Objects.Abstractions;
using MonitorMarkets.Application.Objects.DataBase;
using MonitorMarkets.Databases;

namespace MonitorMarket.Controllers;

[Route("homeController")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly LoggerContext _db;
    IRepository<LogInfo> dbLog;
    
    public HomeController(ILogger<HomeController> logger, IRepository<LogInfo> repositoryLog)
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
    [Microsoft.AspNetCore.Mvc.HttpPost]
    [Microsoft.AspNetCore.Mvc.Route("log/createlog")] 
    [ProducesResponseType(typeof(LogInfo), 200)]
    public IActionResult AddLog([Microsoft.AspNetCore.Mvc.FromBody]LogInfo logInfo)
    {
        var result = dbLog.Create(logInfo);
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
    /// Удаление логов
    /// </summary>
    /// <param name="logInfo"></param>
    /// <response code="200">Log удален из базы данных</response>
    /// <response code="400">неправильные параметры</response>

    [Microsoft.AspNetCore.Mvc.HttpDelete]
    [Microsoft.AspNetCore.Mvc.Route("log/deletelog")]
    [ProducesResponseType(typeof(LogInfo), 200)]

    public IActionResult DelLog([FromQuery]Guid id)
    {
        var result = dbLog.Remove(id);
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
    /// Обновление логов
    /// </summary>
    /// <param name="logInfo"></param>
    /// <response code="200">Log обновлён</response>
    /// <response code="400">неправильные параметры</response>
    [Microsoft.AspNetCore.Mvc.HttpPut]
    [Microsoft.AspNetCore.Mvc.Route("log/updatelog")]
    [ProducesResponseType(typeof(LogInfo), 200)]

    public IActionResult UpLog([FromQuery]Guid id, [Microsoft.AspNetCore.Mvc.FromBody] LogInfo info)
    {
        var result = dbLog.Update(info, id);
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
    /// Поиск лога по id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="200">Log найден</response>
    /// <response code="400">неправильные параметры</response>

    [Microsoft.AspNetCore.Mvc.HttpGet]
    [Microsoft.AspNetCore.Mvc.Route("log/findlog")]
    [ProducesResponseType(typeof(LogInfo), 200)]
    public LogInfo FLog([FromQuery]Guid id)
    {
        var search = dbLog.FindById(id);
        return search;
    }
    
    #endregion
    
    #endregion
}