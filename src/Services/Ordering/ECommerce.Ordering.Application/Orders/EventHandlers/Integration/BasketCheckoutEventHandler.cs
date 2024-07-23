using ECommerce.Ordering.Application.Orders.Commands.CreateOrder;
using ECommerce.Ordering.Domain.Enums;
using ECommerce.Shared.Messaging.Events;
using Mapster;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace ECommerce.Ordering.Application.Orders.EventHandlers.Integration;

public class BasketCheckoutEventHandler(ISender sender, ILogger<BasketCheckoutEventHandler> logger) : IConsumer<BasketCheckoutEvent>
{
    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        //Handle integration event
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

        var command = MapMessageToCommand(context.Message);
        await sender.Send(command);
    }

    private CreateOrderCommand MapMessageToCommand(BasketCheckoutEvent message)
    {
        var addressDto = new AddressDto(
            message.FirstName,
            message.LastName,
            message.EmailAddress,
            message.AddressLine,
            message.Country,
            message.State,
            message.ZipCode);

        var payment = new PaymentDto(
            message.CardName,
            message.CardNumber,
            message.Expiration,
            message.CVV,
            message.PaymentMethod);

        var orderId = Guid.NewGuid();

        var order = new OrderDto(
            orderId,
            message.CustomerId,
            message.UserName,
            addressDto,
            addressDto,
            payment,
            OrderStatus.Pending,
            [
                new OrderItemDto(orderId,new Guid("62f97c80-4520-4ea2-8eae-2ee8a27efae7"),1,1500),
                new OrderItemDto(orderId,new Guid("f4b40d33-5564-409d-81be-62b0f919f7aa"),2,1400),
            ]);

        return new CreateOrderCommand(order);
    }
}
