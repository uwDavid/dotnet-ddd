using System.Reflection;

using Ordering.Application.Data;

using Ordering.Domain.Models;

namespace Ordering.Infrastructure.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    { }

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    // ModelBuilder configuration
    protected override void OnModelCreating(ModelBuilder builder)
    {
        // builder.Entity<Customer>().Property(c => c.Name)
        //     .IsRequired()
        //     .HasMaxLength(100);

        // instead of writing all entity configs here
        // we will create separate config files using this method
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}