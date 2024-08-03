using Carter;
using ECommerce.Web.UI.BlazorSSR.Shared.Models.Catalog;

namespace ECommerce.Web.UI.BlazorSSR.Modules.Products;

public record GetProductsRequest(int? PageNumber = 1, int? PageSize = 10);


public class ProductsModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/catalog-service/products", async ([AsParameters] GetProductsRequest request, ICatalogService catalogService) =>
        {
            var result = await catalogService.GetProducts(request.PageNumber, request.PageSize);
            return Results.Ok(result);

        })
        .WithName("GetProducts")
        .Produces<GetProductsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get all Products")
        .WithDescription("Gets all products from Catalog Db");

        app.MapGet("/catalog-service/products/{id}", async (Guid id, ICatalogService catalogService) =>
        {
            var result = await catalogService.GetProduct(id);
            return Results.Ok(result);

        })
            .WithName("GetProduct")
            .Produces<GetProductsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get product by id")
        .WithDescription("Gets product by id");

        app.MapGet("/catalog-service/products/category/{category}", async (string category, ICatalogService catalogService) =>
        {
            var result = await catalogService.GetProductByCategory(category);
            return Results.Ok(result);

        })
           .WithName("GetProductByCategory")
           .Produces<GetProductsResponse>(StatusCodes.Status200OK)
           .ProducesProblem(StatusCodes.Status400BadRequest)
           .WithSummary("Get product by Category")
           .WithDescription("Gets product by Category");
    }

}