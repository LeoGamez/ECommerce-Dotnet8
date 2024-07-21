using ECommerce.Ordering.Domain.Events;
using Microsoft.Extensions.Logging;

namespace ECommerce.Ordering.Application.Orders.EventHandlers;

public class OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger) : INotificationHandler<OrderCreatedEvent>
{
    public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain event Handled: {DomainEvent}",notification.GetType().Name);
        return Task.CompletedTask;
    }
}

