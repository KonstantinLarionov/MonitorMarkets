using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace MonitorMarkets.Databases;

public class ServiceCollection
{
    public static void AddInfrastructureHandler(this IServiceCollection services)
    {
        services.AddTransient<>()
    }
}