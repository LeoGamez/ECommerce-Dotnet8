using Carter;
using ECommerce.Web.UI.BlazorSSR.Shared.Models.Basket;

namespace ECommerce.Web.UI.BlazorSSR.Modules.Basket;

public class BasketModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket-service/basket/{userName}", async (string userName, IBasketService catalogService) =>
        {
            var result = await catalogService.GetBasket(userName);
            return Results.Ok(result);
        })
        .WithName("GetBasket")
        .Produces<GetBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Basket for User")
        .WithDescription("Get Basket for User");

        app.MapPost("/basket-service/basket", async (StoreBasketRequest storeBasketRequest, IBasketService catalogService) =>
        {
            var result = await catalogService.StoreBasket(storeBasketRequest);
            return Results.Ok(result);

        })
        .WithName("StoreBasket")
        .Produces<StoreBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Store Basket for User")
        .WithDescription("Store Basket for User");

        app.MapDelete("/basket-service/basket/{userName}", async (string userName, IBasketService catalogService) =>
        {
            var result = await catalogService.DeleteBasket(userName);
            return Results.Ok(result);

        })
        .WithName("DeleteBasket")
        .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Delete Basket for User")
        .WithDescription("Delete Basket for User");

        app.MapPost("/basket-service/basket/checkout", async (CheckoutBasketRequest request, IBasketService catalogService) =>
        {
            var result = await catalogService.CheckoutBasket(request);
            return Results.Ok(result);

        })
        .WithName("CheckoutBasket")
        .Produces<CheckoutBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Checkout Basket for User")
        .WithDescription("Checkout Basket for User");
    }
}
