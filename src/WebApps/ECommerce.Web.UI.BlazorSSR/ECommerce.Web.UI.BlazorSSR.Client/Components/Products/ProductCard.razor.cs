
using ECommerce.Web.UI.BlazorSSR.Shared.Models.Catalog;
using Microsoft.AspNetCore.Components;

namespace ECommerce.Web.UI.BlazorSSR.Client.Components.Products;

public partial class ProductCard
{
    [Parameter]
    public ProductModel Product { get; set; }
}
