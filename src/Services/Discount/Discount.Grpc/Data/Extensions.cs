using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data;

public static class Extensions
// static method -> in order to create extension method for IApplicationBuilder
{
    public static IApplicationBuilder UseMigration(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<DiscountContext>();
        dbContext.Database.MigrateAsync();

        return app;
    }
}