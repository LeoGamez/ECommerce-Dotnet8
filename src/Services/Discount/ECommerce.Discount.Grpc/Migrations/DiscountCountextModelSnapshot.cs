﻿// <auto-generated />
using ECommerce.Discount.Grpc.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ECommerce.Discount.Grpc.Migrations
{
    [DbContext(typeof(DiscountCountext))]
    partial class DiscountCountextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.7");

            modelBuilder.Entity("ECommerce.Discount.Grpc.Models.Coupon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("Amount")
                        .HasColumnType("REAL");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Coupons");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amount = 10.0,
                            Description = "Iphone Discount",
                            ProductName = "Iphone X"
                        },
                        new
                        {
                            Id = 2,
                            Amount = 10.0,
                            Description = "Samsung Discount",
                            ProductName = "Samsung 10"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
