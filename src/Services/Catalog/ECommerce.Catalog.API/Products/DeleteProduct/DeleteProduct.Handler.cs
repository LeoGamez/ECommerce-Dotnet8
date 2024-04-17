
namespace ECommerce.Catalog.API.Products.DeleteProduct;

public record DeleteProductCommand(Guid Id) : ICommand;

internal class DeleteProductHandler(IDocumentSession session, ILogger<DeleteProductHandler> logger) : ICommandHandler<DeleteProductCommand>
{
    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        session.Delete<Product>(request.Id);
        await session.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}