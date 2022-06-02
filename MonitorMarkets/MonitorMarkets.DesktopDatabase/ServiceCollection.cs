using Microsoft.Extensions.DependencyInjection;
using MonitorMarkets.Application.Objects.Abstractions;
using MonitorMarkets.DesktopDatabase.Entities;
using MonitorMarkets.DesktopDatabase.Repositories;

namespace MonitorMarkets.DesktopDatabase;

public static class ServiceCollection
{
    public static void AddStrategyService(this IServiceCollection service)
    {
        service.AddTransient<IRepository<ConnectionKeys>,KeysRepository>();
    }

}