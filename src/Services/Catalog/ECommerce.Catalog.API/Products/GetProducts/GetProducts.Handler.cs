using ECommerce.Catalog.API.Products.CreateProduct;
using ECommerce.Shared.Abstractions;

namespace ECommerce.Catalog.API.Products.GetProducts;

public record GetProductsQuery(): IQuery<GetProductsResult>;

public record GetProductsResult(IEnumerable<Product> Products);

internal class GetProductsQueryHandler(IDocumentSession session, ILogger<GetProductsQueryHandler> logger)
    : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductsQueryHandler.Handle called with {@Query}", query);

        //Get products from database
        var products = await session.Query<Product>().ToListAsync(cancellationToken);
       
        //Return Result
       return new GetProductsResult(products);
    }
}