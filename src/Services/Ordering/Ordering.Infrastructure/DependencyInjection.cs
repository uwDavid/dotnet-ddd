using Microsoft.EntityFrameworkCore.Diagnostics;
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

        // Add service to container - to DI MediatR for DomainEvent Interceptor
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        // now use service provider to get the above services 
        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            // options.AddInterceptors(new AuditableEntityInterceptor());
            // new DispatchDomainEventsInterceptor());
            // but we can't add MediatR this way

            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseNpgsql(connectingString);

            // we still have to inject MediatR => this is done in API Program.cs
            // must be before .AddInfrastructure layer
        });


        return services;
    }
}