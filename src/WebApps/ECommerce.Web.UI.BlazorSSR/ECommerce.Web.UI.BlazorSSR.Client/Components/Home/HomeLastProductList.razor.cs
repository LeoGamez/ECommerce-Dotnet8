
using ECommerce.Web.UI.BlazorSSR.Shared.Models.Catalog;
using Microsoft.AspNetCore.Components;

namespace ECommerce.Web.UI.BlazorSSR.Client.Components.Home;

public partial class HomeLastProductList
{
    [Inject]
    public ICatalogService CatalogService { get; set; }

    private List<ProductModel> products = new List<ProductModel>();

    protected async override Task OnInitializedAsync()
    {
        try
        {
            var repsonse = await CatalogService.GetProducts(1, 10);
            products.AddRange(repsonse.Products);
        }
        catch (Exception ex)
        {
            var m = ex;
        }
    }
}
