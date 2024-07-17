using ECommerce.Discount.Grpc.Data;
using ECommerce.Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Discount.Grpc.Services;
public class DiscountService(DiscountContext discountContext,ILogger<DiscountService> logger) : DiscountProto.DiscountProtoBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon= await discountContext.Coupons.FirstOrDefaultAsync(c=>c.ProductName==request.ProductName);
        if (coupon == null)
        {
            coupon = new()
            {
                ProductName = "No discount",
                Description = "No discount description.",
                Amount = 0
            };
        }

        logger.LogInformation("Discount is retrieved for ProductName:{productName}, Amount={amount}",coupon.ProductName,coupon.Amount);

        return coupon.Adapt<CouponModel>();
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();
    
        if(coupon is null)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Request"));
        }

        discountContext.Coupons.Add(coupon);
        await discountContext.SaveChangesAsync();

        logger.LogInformation("Discount was succesfully created with Id= {id} :{productName}, Amount={amount}",coupon.Id, coupon.ProductName, coupon.Amount);

        return coupon.Adapt<CouponModel>();
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();
        if (coupon is null)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Request"));
        }

        discountContext.Coupons.Update(coupon);
        await discountContext.SaveChangesAsync();

        logger.LogInformation("Discount was succesfully updated with Id= {id} :{productName}, Amount={amount}", coupon.Id, coupon.ProductName, coupon.Amount);

        return coupon.Adapt<CouponModel>();
    }

    public override async Task<DeletediscountResponse> DeleteDiscount(DeletediscountRequest request, ServerCallContext context)
    {
        var coupon = await discountContext.Coupons.FirstOrDefaultAsync(c => c.ProductName == request.ProductName);
        if (coupon is null)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Request"));
        }

        discountContext.Coupons.Remove(coupon);
        await discountContext.SaveChangesAsync();

        logger.LogInformation("Discount was succesfully deleted with Id= {id} :{productName}, Amount={amount}", coupon.Id, coupon.ProductName, coupon.Amount);

        return new() { Success=true };
    }
}
