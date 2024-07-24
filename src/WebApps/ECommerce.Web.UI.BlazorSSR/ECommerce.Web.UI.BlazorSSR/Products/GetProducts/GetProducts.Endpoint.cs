using Carter;
using ECommerce.Web.UI.BlazorSSR.Shared.Models.Catalog;

namespace ECommerce.Web.UI.BlazorSSR.Products.GetProducts;

public record GetProductsRequest(int? PageNumber = 1, int? PageSize = 10);


public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/catalog-service/products", async ([AsParameters] GetProductsRequest request, ICatalogService catalogService) =>
        {
            var result = await catalogService.GetProducts(request.PageNumber,request.PageSize);
            return Results.Ok(result);

        })
        .WithName("GetProducts")
        .Produces<GetProductsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get all Products")
        .WithDescription("Gets all products from Catalog Db");
    }
}