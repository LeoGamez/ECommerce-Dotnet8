﻿using ECommerce.Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Discount.Grpc.Data;
public class DiscountContext : DbContext
{
    public DbSet<Coupon> Coupons { get; set; }

    public DiscountContext(DbContextOptions<DiscountContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coupon>().HasData(
            new Coupon
            {
                Id = 1,
                ProductName = "Iphone X",
                Description = "Iphone Discount",
                Amount = 10
            },
            new Coupon
            {
                Id = 2,
                ProductName = "Samsung 10",
                Description = "Samsung Discount",
                Amount = 10
            }
        );
    }
}
