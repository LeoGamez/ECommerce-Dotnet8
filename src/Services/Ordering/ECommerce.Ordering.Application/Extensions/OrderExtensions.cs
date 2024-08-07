﻿using ECommerce.Ordering.Application.Orders.Commands.CreateOrder;
using ECommerce.Ordering.Domain.Enums;
using ECommerce.Ordering.Domain.Events;

namespace ECommerce.Ordering.Application.Extensions;

public static class OrderExtensions
{
    public static List<OrderDto> MapOrdersToDto(this List<Order> orders)
    {
        return orders.Select(order => new OrderDto(
                Id: order.Id.Value,
                CustomerId: order.CustomerId.Value,
                OrderName: order.OrderName.Value,
                ShippingAddress: new AddressDto(
                        FirstName: order.ShippingAddress.FirstName,
                        LastName: order.ShippingAddress.LastName,
                        EmailAddress: order.ShippingAddress.EmailAddress,
                        AddressLine: order.ShippingAddress.AddressLine,
                        Country: order.ShippingAddress.Country,
                        State: order.ShippingAddress.State,
                        ZipCode: order.ShippingAddress.ZipCode
                    ),
                BillingAddress: new AddressDto(
                        FirstName: order.BillingAddress.FirstName,
                        LastName: order.BillingAddress.LastName,
                        EmailAddress: order.BillingAddress.EmailAddress,
                        AddressLine: order.BillingAddress.AddressLine,
                        Country: order.BillingAddress.Country,
                        State: order.BillingAddress.State,
                        ZipCode: order.BillingAddress.ZipCode
                    ),
                Payment: new PaymentDto(
                        CardName: order.Payment.CardName,
                        CardNumber: order.Payment.CardNumber,
                        Expiration: order.Payment.Expiration,
                        Cvv: order.Payment.CVV,
                        PaymentMethod: order.Payment.PaymentMethod
                    ),
                OrderStatus: order.OrderStatus,
                OrderItems: order.OrderItems.Select(oi => new OrderItemDto(
                        OrderId: oi.OrderId.Value,
                        ProductId: oi.ProductId.Value,
                        Quantity: oi.Quantity,
                        Price: oi.Price)).ToList()
            )).ToList();
    }

    public static OrderDto ToOrderDto(this OrderCreatedEvent message)
    {
        var order = message.order;

        var shippingAddressDto = new AddressDto(
            order.ShippingAddress.FirstName,
            order.ShippingAddress.LastName,
            order.ShippingAddress.EmailAddress,
            order.ShippingAddress.AddressLine,
            order.ShippingAddress.Country,
            order.ShippingAddress.State,
            order.ShippingAddress.ZipCode);

        var billingAddressDto = new AddressDto(
            order.BillingAddress.FirstName,
            order.BillingAddress.LastName,
            order.BillingAddress.EmailAddress,
            order.BillingAddress.AddressLine,
            order.BillingAddress.Country,
            order.BillingAddress.State,
            order.BillingAddress.ZipCode);

        var payment = new PaymentDto(
             order.Payment.CardName,
             order.Payment.CardNumber,
             order.Payment.Expiration,
             order.Payment.CVV,
             order.Payment.PaymentMethod);

        var orderId = Guid.NewGuid();

        var orderDto = new OrderDto(
            orderId,
            order.CustomerId.Value,
            order.OrderName.Value,
            shippingAddressDto,
            billingAddressDto,
            payment,
            OrderStatus.Pending,
            order.OrderItems.Select(oi => new OrderItemDto(oi.OrderId.Value, oi.ProductId.Value, oi.Quantity, oi.Price)).ToList());

        return orderDto;
    }
}
