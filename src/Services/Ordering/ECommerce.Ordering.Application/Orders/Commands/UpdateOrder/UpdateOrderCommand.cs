﻿using FluentValidation;

namespace ECommerce.Ordering.Application.Orders.Commands.UpdateOrder;

public record UpdateOrderResult(bool IsSuccess);
public record UpdateOrderCommand(OrderDto Order) : ICommand<UpdateOrderResult>;
public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("Name is Required");
        RuleFor(x => x.Order.CustomerId).NotEmpty().WithMessage("CustomerId is Required");
        RuleFor(x => x.Order.OrderItems).NotEmpty().WithMessage("Order Items should not be empty");
    }
}
