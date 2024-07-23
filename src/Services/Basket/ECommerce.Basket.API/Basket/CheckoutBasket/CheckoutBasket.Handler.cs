
using ECommerce.Shared.Messaging.Events;
using MassTransit;

namespace ECommerce.Basket.API.Basket.CheckoutBasket;

public record CheckoutBasketCommand(CheckoutBasketDto CheckoutBasket) : ICommand<CheckoutBasketResult>;

public record CheckoutBasketResult(bool IsSuccess);

public class CheckoutBasketCommandValidator: AbstractValidator<CheckoutBasketCommand>
{
    public CheckoutBasketCommandValidator()
    {
        RuleFor(x => x.CheckoutBasket).NotNull().WithMessage("BasketCheckout should not be null");
        RuleFor(x => x.CheckoutBasket.UserName).NotEmpty().WithMessage("BasketCheckout UserName should not be empty");
    }
}

public class CheckoutBasketCommandHandler(IBasketRepository basketRepository,IPublishEndpoint publishEndpoint) : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
{
    public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
    {
        var basket=await basketRepository.GetShoppingCart(command.CheckoutBasket.UserName, cancellationToken);
        if (basket == null)
        {
            return new(false);
        }

        var eventMessage = command.CheckoutBasket.Adapt<BasketCheckoutEvent>();
        eventMessage.TotalPrice = basket.TotalPrice;

        await publishEndpoint.Publish(eventMessage,cancellationToken);

        await basketRepository.DeleteBasket(command.CheckoutBasket.UserName);

        return new (true);
    }
}