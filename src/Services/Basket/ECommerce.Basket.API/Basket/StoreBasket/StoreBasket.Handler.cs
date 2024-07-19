
using ECommerce.Discount.Grpc;

namespace ECommerce.Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;

public record StoreBasketResult(string UserName);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Cart can't be null");
        RuleFor(x => x.Cart.Username).NotEmpty().WithMessage("Username is required");
    }
}

public class StoreBasketCommandHandler(IBasketRepository repository, DiscountProto.DiscountProtoClient discountProtoClient) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        await DeductDiscount(discountProtoClient, command);

        _ = await repository.StoreBasket(command.Cart, cancellationToken);

        return new StoreBasketResult(command.Cart.Username);
    }

    private static async Task DeductDiscount(DiscountProto.DiscountProtoClient discountProtoClient, StoreBasketCommand command)
    {
        foreach (var item in command.Cart.Items)
        {
            var coupon = await discountProtoClient.GetDiscountAsync(new() { ProductName = item.ProductName });
            item.Price -= (decimal)coupon.Amount;
        }
    }

}
