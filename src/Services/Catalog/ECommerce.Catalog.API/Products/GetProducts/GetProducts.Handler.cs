using ECommerce.Catalog.API.Products.CreateProduct;
using ECommerce.Shared.Abstractions;

namespace ECommerce.Catalog.API.Products.GetProducts;

public record GetProductsQuery(): IQuery<GetProductsResult>;

public record GetProductsResult(IEnumerable<Product> Products);

internal class GetProductsQueryHandler(IDocumentSession session)
    : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        //Get products from database
        var products = await session.Query<Product>().ToListAsync(cancellationToken);
       
        //Return Result
       return new GetProductsResult(products);
    }
}