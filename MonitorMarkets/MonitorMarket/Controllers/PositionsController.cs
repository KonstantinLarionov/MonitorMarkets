using Microsoft.AspNetCore.Mvc;
using MonitorMarkets.Application.Objects.Abstractions;
using MonitorMarkets.Application.Objects.DataBase;
using MonitorMarkets.Databases;

namespace MonitorMarket.Controllers;

[Route("positionsController")]
public class PositionsController : Controller
{
    private readonly ILogger<PositionsController> _logger;
    private readonly LoggerContext _db;
    IRepository<PositionsEntitiesInfo> dbPositions;

    public PositionsController(ILogger<PositionsController> logger,
        IRepository<PositionsEntitiesInfo> repositoryPositions)
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

    [Microsoft.AspNetCore.Mvc.HttpPost]
    [Microsoft.AspNetCore.Mvc.Route("positions/addpositions")]
    [ProducesResponseType(typeof(PositionsEntitiesInfo), 200)]

    public IActionResult AddPositions([Microsoft.AspNetCore.Mvc.FromBody]PositionsEntitiesInfo posInfo)
    {
        var result = dbPositions.Create(posInfo);
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
    /// Удаление позиций
    /// </summary>
    /// <param name="posInfo"></param>
    /// <response code="200">Positions удален из базы данных</response>
    /// <response code="400">неправильные параметры</response>

    [Microsoft.AspNetCore.Mvc.HttpDelete]
    [Microsoft.AspNetCore.Mvc.Route("positions/deletepositions")]
    [ProducesResponseType(typeof(PositionsEntitiesInfo), 200)]

    public IActionResult DelPositions([FromQuery]Guid id)
    {
        var result = dbPositions.Remove(id);
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
    /// Обновление позиций
    /// </summary>
    /// <param name="posInfo"></param>
    /// <response code="200">Positions обновлён</response>
    /// <response code="400">неправильные параметры</response>

    [Microsoft.AspNetCore.Mvc.HttpPut]
    [Microsoft.AspNetCore.Mvc.Route("positions/updatepositions")]
    [ProducesResponseType(typeof(PositionsEntitiesInfo), 200)]

    public IActionResult UpPositions([FromQuery]Guid id, [Microsoft.AspNetCore.Mvc.FromBody]PositionsEntitiesInfo posInfo)
    {
        var result = dbPositions.Update(posInfo, id);
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
    /// Поиск позиций по id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="200">Positions найден</response>
    /// <response code="400">неправильные параметры</response>

    [Microsoft.AspNetCore.Mvc.HttpGet]
    [Microsoft.AspNetCore.Mvc.Route("positions/findpositions")]
    [ProducesResponseType(typeof(PositionsEntitiesInfo), 200)]

    public PositionsEntitiesInfo FPositions([FromQuery]Guid id)
    {
        var search = dbPositions.FindById(id);
        return search;
    }


    #endregion
    
    #endregion
    
}