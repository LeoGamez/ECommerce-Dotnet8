using ECommerce.Shared.Abstractions;


namespace ECommerce.Catalog.API.Products.CreateProduct;

public record CreateProductResult(Guid Id);
public record CreateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<CreateProductResult>;

internal class CreateProductCommandHandler(IDocumentSession session)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        //Create Product Entity
        var product = new Product()
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price,
        };

        //Save to database
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);

        //Return Result
        return new(product.Id);
    }
}