namespace ECommerce.Ordering.Application.Orders.Commands.DeleteOrder;

public record DeleteOrderCommand(Guid Id) : ICommand<DeleteOrderResult>;
public record DeleteOrderResult(bool IsSuccess);
