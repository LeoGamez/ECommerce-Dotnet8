namespace ECommerce.Ordering.Domain.Events;

public record OrderCreatedEvent(Order order) : IDomainEvent;

