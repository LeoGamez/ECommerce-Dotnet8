using ECommerce.Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Discount.Grpc.Data;
public class DiscountCountext : DbContext
{
    public DbSet<Coupon> Coupons { get; set; }

    public DiscountCountext(DbContextOptions<DiscountCountext> options) : base(options)
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
