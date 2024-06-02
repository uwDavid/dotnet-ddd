using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Ordering.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices
        (this IServiceCollection services, IConfiguration configuration)
    {
        var connectingString = configuration.GetConnectionString("Database");

        //     services.AddDbContext<ApplicationDbContext>(options =>
        //     {
        //     options.UseSqlServer(connectingString));
        //       });
        return services;
    }
}