using ECommerce.Shared.Abstractions;
using FluentValidation;

namespace ECommerce.Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<Unit>;

public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is Required");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is Required").Length(2,150).WithMessage("Name must be between 2 and 150 characters");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Category is Required");
        RuleFor(x => x.Price).NotEmpty().WithMessage("Price is Required").GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}
internal class UpdateProductCommandHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommand, Unit>
{
    public async Task<Unit> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(command.Id);
        
        if(product == null)
        {
            throw new ProductNotFoundException();
        }
        product = command.Adapt<Product>();
        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}