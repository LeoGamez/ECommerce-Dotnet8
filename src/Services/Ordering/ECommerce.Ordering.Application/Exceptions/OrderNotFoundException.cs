using ECommerce.Shared.Exceptions;

namespace ECommerce.Ordering.Application.Exceptions;

public class OrderNotFoundException : NotFoundException 
{
    public OrderNotFoundException(Guid id) : base("Order",id) { }
}
