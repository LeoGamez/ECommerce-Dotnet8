using ECommerce.Web.UI.BlazorSSR.Models.Catalog;

namespace ECommerce.Web.UI.BlazorSSR.Services;

public interface ICatalogService
{
    [Get("/catalog-service/products?pageNumber={pageNumber}&pageSize=={pageSize}")]
    Task<GetProductsResponse> GetProducts(int? pageNumber = 1, int? pageSize = 10);
    [Get("/catalog-service/products/{id}")]
    Task<GetProductsResponse> GetProduct(Guid id);
    [Get("/catalog-service/products/category/{category}")]
    Task<GetProductsResponse> GetProductByCategory(string category);
}
