using ECommerce.Web.UI.BlazorSSR.Shared.Models.Catalog;

namespace ECommerce.Web.UI.BlazorSSR.Shared.Services;

public interface ICatalogService
{
    [Get("/catalog-service/products")]
    Task<GetProductsResponse> GetProducts([AliasAs("PageNumber")] int? pageNumber = null, [AliasAs("PageSize")] int? pageSize = null);
    [Get("/catalog-service/products/{id}")]
    Task<GetProductsResponse> GetProduct(Guid id);
    [Get("/catalog-service/products/category/{category}")]
    Task<GetProductsResponse> GetProductByCategory(string category);
}
