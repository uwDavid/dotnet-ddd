namespace Ordering.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        // services.AddCarter();
        return services;
    }

    // Write an extension method to WebApplication class
    public static WebApplication UseApiServices(this WebApplication app)
    {
        // app.MapCarter();
        return app;
    }
}