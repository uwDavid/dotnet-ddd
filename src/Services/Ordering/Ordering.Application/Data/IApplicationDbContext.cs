using Microsoft.EntityFrameworkCore;

using Ordering.Domain.Models;

namespace Ordering.Application.Data;

// use interface to make application layer independent of infrastructure layer
// DbSet class comes from EntityFrameworkCore
// we just use this to represent the abstraction over Infrastructure layer
public interface IApplicationDbContext
{
    DbSet<Customer> Customers { get; }
    DbSet<Product> Products { get; }
    DbSet<Order> Orders { get; }
    DbSet<OrderItem> OrderItems { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}