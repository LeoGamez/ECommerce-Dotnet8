using Grpc.Core;

namespace ECommerce.Discount.Grpc.Services;
public class DiscountService : DiscountProto.DiscountProtoBase
{
    public override Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        return base.GetDiscount(request, context);
    }

    public override Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        return base.CreateDiscount(request, context);
    }

    public override Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        return base.UpdateDiscount(request, context);
    }

    public override Task<DeletediscountResponse> DeleteDiscount(DeletediscountRequest request, ServerCallContext context)
    {
        return base.DeleteDiscount(request, context);
    }
}
