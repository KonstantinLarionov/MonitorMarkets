using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MonitorMarkets.Application.Objects.DataBase;
using Microsoft.AspNetCore.Mvc.Core;


namespace MonitorMarkets.Databases
{
    public static class ServiceCollection
    {
        public static void AddInfrastructureHandler(this IServiceCollection services)
        {
            services.AddTransient<IRespository<LogInfo>>();
        }
    }
}