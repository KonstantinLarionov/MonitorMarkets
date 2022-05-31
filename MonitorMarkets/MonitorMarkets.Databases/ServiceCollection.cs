using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MonitorMarkets.Application.Objects.DataBase;
using Microsoft.AspNetCore.Mvc.Core;
using MonitorMarkets.Application.Objects.Abstractions;
using MonitorMarkets.Databases.Repositories;


namespace MonitorMarkets.Databases
{
    public static class ServiceCollection
    {
        public static void AddInfrastructureDataBase(this IServiceCollection services)
        {
            services.AddEntityFrameworkMySql().AddDbContext<LoggerContext>();
            services.AddTransient<IRepository<LogInfo>, LoggerRepository>();
            services.AddTransient<IRepository<OrdersEntitiesInfo>, OrderRepository>();
            services.AddTransient<IRepository<PositionsEntitiesInfo>, PositionsRepository>();
            services.AddTransient<IRepository<WalletEntitiesInfo>, WalletRepository>();
        }
    }
}