﻿
using ECommerce.Ordering.Domain.Models;
using ECommerce.Ordering.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Ordering.Infrastructure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .HasConversion(
                productId => productId.Value,
                dbId => ProductId.Of(dbId));

        builder.Property(p=>p.Name).HasMaxLength(100).IsRequired();
    }
}
