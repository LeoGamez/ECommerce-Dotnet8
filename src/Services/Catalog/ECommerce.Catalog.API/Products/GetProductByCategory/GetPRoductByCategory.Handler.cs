
using FluentValidation;

namespace ECommerce.Catalog.API.Products.GetProductByCategory;

public record GetProductByCategoryQuery(string Category): IQuery<GetProductByCategoryResult>;

public record GetProductByCategoryResult(IEnumerable<Product> Products);

public class GetByCategoryProductValidator : AbstractValidator<GetProductByCategoryQuery>
{
    public GetByCategoryProductValidator()
    {
        RuleFor(x => x.Category).NotEmpty().WithMessage("Category is Required");
    }
}

internal class GetProductByCategoryHandler(IDocumentSession session) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        var result = await session.Query<Product>()
            .Where(p => p.Category.Contains(query.Category))
            .ToListAsync();

        return new GetProductByCategoryResult(result);
    }
}