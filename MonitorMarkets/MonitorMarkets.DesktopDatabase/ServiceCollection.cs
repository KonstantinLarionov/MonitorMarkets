using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MonitorMarkets.Application.Objects.Abstractions;
using MonitorMarkets.DesktopDatabase.Entities;
using MonitorMarkets.DesktopDatabase.Repositories;

namespace MonitorMarkets.DesktopDatabase;

public static class ServiceCollection
{
    public static void AddDesktopDataBase(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddDbContextPool<DesktopContext>(options =>
            options.UseMySql(configuration.GetConnectionString("DefaultConnection"),
                new MySqlServerVersion(new Version(5, 6, 45))));
        service.AddTransient<IRepository<ConnectionKeys>,KeysRepository>();
    }

}