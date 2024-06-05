using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        // Id comes from the Entity abstract class
        builder.HasKey(c => c.Id);
        // use Id as Primary Key for Customer table
        // hovering over Id, this Id is a value object 
        // we need to specify custom conversions for this Id => so EF Core knows how to store this value
        builder.Property(c => c.Id).HasConversion(
            customerId => customerId.Value,
            dbId => CustomerId.Of(dbId)
        );
        // customerId => customerId.Value 
        // use customerId.Value when storing info to db
        // dbId => CustomerId.Of(dbId)
        // use CustomerId.Of() when reading Id value from db, convert to CustomerId

        builder.Property(c => c.Name).HasMaxLength(100).IsRequired();
        builder.Property(c => c.Email).HasMaxLength(255);
        builder.HasIndex(c => c.Email).IsUnique();
    }
}