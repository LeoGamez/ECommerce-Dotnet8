
namespace ECommerce.Catalog.API.Products.DeleteProduct;

public record DeleteProductResponse();

public class DeleteProductEndpoint(ISender sender) : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id}", async (Guid id, ISender sender) => {

            await sender.Send(new DeleteProductCommand(id));

            return Results.Accepted();
        });
    }
}
