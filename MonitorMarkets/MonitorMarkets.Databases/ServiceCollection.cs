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
            services.AddTransient<IRepository<LogInfo>, LoggerRepository<LogInfo>>();
            services.AddTransient<IRepository<OrdersEntitiesInfo>, LoggerRepository<OrdersEntitiesInfo>>();
            services.AddTransient<IRepository<PositionsEntitiesInfo>, LoggerRepository<PositionsEntitiesInfo>>();
            services.AddTransient<IRepository<WalletEntitiesInfo>, LoggerRepository<WalletEntitiesInfo>>();
        }
    }
}