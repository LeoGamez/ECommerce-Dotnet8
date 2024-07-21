
using ECommerce.Ordering.Application.Orders.Commands.CreateOrder;

namespace ECommerce.Ordering.API.Endpoints;

public record CreateOrderRequest(OrderDto Order);
public record CreateOrderResponse(Guid id);
public class CreateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/orders", async (CreateOrderRequest request, ISender sender) => {

            var result = await sender.Send(request.Adapt<CreateOrderCommand>());
            var response = result.Adapt<CreateOrderResponse>();

            return Results.Created($"/orders/{response.id}", response);
        })
            .WithName("CreateOrder")
            .Produces<CreateOrderResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create an Order")
            .WithDescription("Create an order");
    }
}
