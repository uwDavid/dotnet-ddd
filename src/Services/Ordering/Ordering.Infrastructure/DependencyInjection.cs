using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices
        (this IServiceCollection services, IConfiguration configuration)
    {
        // get connection string from Order.API appsetting.json
        var connectingString = configuration.GetConnectionString("Database");

        // Add service to container
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectingString));

        return services;
    }
}