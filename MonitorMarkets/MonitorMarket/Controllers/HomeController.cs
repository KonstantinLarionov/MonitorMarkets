using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MonitorMarket.Models;
using MonitorMarkets.Application.Objects.Abstractions;
using MonitorMarkets.Application.Objects.DataBase;
using MonitorMarkets.Databases;
using MonitorMarkets.Databases.Entities;

namespace MonitorMarket.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly LoggerContext _db;
    IRepository<LogInfo> dbLog;
    IRepository<OrdersEntitiesInfo> dbOrder;
    IRepository<PositionsEntitiesInfo> dbPositions;
    IRepository<WalletEntitiesInfo> dbWallet;
    
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
        
        dbLog = repositoryLog;
        dbOrder = repositoryOrder;
        dbPositions = repositoryPositions;
        dbWallet = repositoryWallet;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}