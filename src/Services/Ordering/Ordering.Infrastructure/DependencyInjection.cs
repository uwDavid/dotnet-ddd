using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Ordering.Infrastructure.Data.Interceptors;

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
        {
            options.AddInterceptors(new AuditableEntityInterceptor());
            // new DispatchDomainEventsInterceptor());
            // but we can't add MediatR this way
            options.UseNpgsql(connectingString);
        });

        return services;
    }
}