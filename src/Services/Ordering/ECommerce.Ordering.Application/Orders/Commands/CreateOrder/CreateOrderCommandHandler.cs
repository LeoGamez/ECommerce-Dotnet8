

namespace ECommerce.Ordering.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler(IApplicationDbContext dbContext) 
    : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public async Task<CreateOrderResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order= CreateNewOrder(request.Order);

        dbContext.Orders.Add(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateOrderResult(order.Id.Value);
    }

    private Order CreateNewOrder(OrderDto orderDto)
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

        var order=  Order.Create(
            OrderId.Of(Guid.NewGuid()),
            CustomerId.Of(orderDto.CustomerId),
            OrderName.Of(orderDto.OrderName),
            shippingAddress,
            billingAddress,
            payment);

        foreach(var orderItemDto in orderDto.OrderItems)
        {
            order.Add(ProductId.Of(orderItemDto.ProductId),orderItemDto.Quantity,orderItemDto.Price);
        }

        return order;
    }
}
