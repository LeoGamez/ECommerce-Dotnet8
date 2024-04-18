using ECommerce.Shared.Abstractions;
using FluentValidation;


namespace ECommerce.Catalog.API.Products.CreateProduct;

public record CreateProductResult(Guid Id);
public record CreateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<CreateProductResult>;
public class CreateProductValidator: AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is Required")
            .Length(2, 150)
            .WithMessage("Name must be between 2 and 150 characters"); ;
        RuleFor(x => x.Category)
            .NotEmpty()
            .WithMessage("Category is Required");
        RuleFor(x => x.ImageFile)
            .NotEmpty()
            .WithMessage("ImageFile is Required");
        RuleFor(x => x.Price)
            .NotEmpty()
            .WithMessage("Price is Required")
            .GreaterThan(0)
            .WithMessage("Price must be greater than 0");
    }
}

internal class CreateProductCommandHandler(IDocumentSession session,ILogger<CreateProductCommandHandler> logger)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Create Product Command Handler called");

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