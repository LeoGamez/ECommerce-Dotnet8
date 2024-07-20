using MediatR;

namespace ECommerce.Ordering.Domain.Abstractions;

public interface IDomainEvent : INotification
{
    Guid Eventd=> Guid.NewGuid();
    public DateTime OcurredOn => DateTime.Now;
    public string EventType => GetType().AssemblyQualifiedName;
}
