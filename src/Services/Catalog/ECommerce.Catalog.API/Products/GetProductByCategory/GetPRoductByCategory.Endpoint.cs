﻿
using ECommerce.Catalog.API.Products.CreateProduct;

namespace ECommerce.Catalog.API.Products.GetProductByCategory;

//public record GetProductByCategoryRequest

public record GetProductByCategoryResponse(IEnumerable<Product> Products);

public class GetProductByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", async (string Category, ISender sender) =>
        {

            var result = await sender.Send(new GetProductByCategoryQuery(Category));

            return Results.Ok(result.Adapt<GetProductByCategoryResponse>());
        })
        .WithName("GetProductByCategory")
        .Produces<CreateProductResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Product By Category")
        .WithDescription("Gets Products By a given Category");

    }
}
