using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MonitorMarkets.Application.Objects.DataBase;
using Microsoft.AspNetCore.Mvc.Core;
using MonitorMarkets.Application.Objects.Abstractions;


namespace MonitorMarkets.Databases
{
    public static class ServiceCollection
    {
        public static void AddInfrastructureDataBase(this IServiceCollection services)
        {
            services.AddEntityFrameworkMySql().AddDbContext<LoggerContext>();
            services.AddTransient<IRepository<LogInfo>>();
            services.AddTransient<IRepository<OrdersEntitiesInfo>>();
            services.AddTransient<IRepository<PositionsEntitiesInfo>>();
            services.AddTransient<IRepository<WalletEntitiesInfo>>();
        }
    }
}