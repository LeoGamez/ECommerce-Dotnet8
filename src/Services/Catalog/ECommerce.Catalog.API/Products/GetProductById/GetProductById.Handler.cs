
using FluentValidation;

namespace ECommerce.Catalog.API.Products.GetProductById;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

public record GetProductByIdResult(Product Product);

public class GetByIdProductValidator : AbstractValidator<GetProductByIdQuery>
{
    public GetByIdProductValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is Required");
    }
}

internal class GetProductByIdQueyHandler(IDocumentSession session, ILogger<GetProductByIdQueyHandler> logger) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(query.Id);

        if (product == null)
        {
            throw new ProductNotFoundException();
        }

        return new GetProductByIdResult(product);
    }
}
