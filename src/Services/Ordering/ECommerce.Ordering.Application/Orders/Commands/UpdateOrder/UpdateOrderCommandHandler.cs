namespace ECommerce.Ordering.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderCommandHandler(IApplicationDbContext dbContext)
    : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
{
    public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        var orderId = OrderId.Of(command.Order.Id);
        var order= await dbContext.Orders.FindAsync([orderId],cancellationToken:cancellationToken);

        if(order is null)
        {
            throw new OrderNotFoundException(command.Order.Id);
        }

        MapUpdatedOrder(order,command.Order);

        dbContext.Orders.Update(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateOrderResult(true);
    }

    private void MapUpdatedOrder(Order order,OrderDto orderDto)
    {
        var requestShppingAddress = orderDto.ShippingAddress;
        var shippingAddress = Address.Of(
            requestShppingAddress.FirstName,
            requestShppingAddress.LastName,
            requestShppingAddress.EmailAddress,
            requestShppingAddress.AddressLine,
            requestShppingAddress.Country,
            requestShppingAddress.State,
            requestShppingAddress.ZipCode);

        var requestBillingAddress = orderDto.BillingAddress;
        var billingAddress = Address.Of(
            requestBillingAddress.FirstName,
            requestBillingAddress.LastName,
            requestBillingAddress.EmailAddress,
            requestBillingAddress.AddressLine,
            requestBillingAddress.Country,
            requestBillingAddress.State,
            requestBillingAddress.ZipCode);

        var requestPayment = orderDto.Payment;
        var payment = Payment.Of(
            requestPayment.CardName,
            requestPayment.CardNumber,
            requestPayment.Expiration,
            requestPayment.Cvv,
            requestPayment.PaymentMethod);

         order.Update(
            OrderName.Of(orderDto.OrderName),
            shippingAddress,
            billingAddress,
            payment,
            orderDto.OrderStatus);
    }
}
