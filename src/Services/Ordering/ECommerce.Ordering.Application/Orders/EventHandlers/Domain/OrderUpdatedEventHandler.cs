using ECommerce.Ordering.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerce.Ordering.Application.Orders.EventHandlers.Domain;

public class OrderUpdatedEventHandler(ILogger<OrderUpdatedEventHandler> logger) : INotificationHandler<OrderUpdatedEvent>
{
    public Task Handle(OrderUpdatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain event Handled: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}

