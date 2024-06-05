using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        // primary key config
        builder.HasKey(oi => oi.Id);
        builder.Property(oi => oi.Id).HasConversion(
            orderItemId => orderItemId.Value,
            dbId => OrderItemId.Of(dbId)
        );

        // foreign key config 
        builder.HasOne<Product>()
            .WithMany()
            .HasForeignKey(oi => oi.ProductId);
        // one product has many orderItems

        builder.Property(oi => oi.Quantity).IsRequired();
        builder.Property(oi => oi.Price).IsRequired();

    }
}