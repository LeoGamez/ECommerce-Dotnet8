using ECommerce.Web.UI.BlazorSSR.Shared.Models.Basket;
using Microsoft.AspNetCore.Components;

namespace ECommerce.Web.UI.BlazorSSR.Client.Components.Home;

public partial class HomeShoppingCartLink
{

    [Inject] IBasketService BasketService { get; set; } = default!;

    int itemQuantity = 0;
    string user = "Test1";

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await GetBasket();
        }
    }

    private async Task GetBasket()
    {
        try
        {
            var basket = await BasketService.GetBasket(user);
            itemQuantity = basket.Cart.Items.Count;
        }
        catch
        {
            await BasketService.StoreBasket(new StoreBasketRequest(new()
            {
                UserName = "Test1",
                Items = new(),
            }));
        }

        await InvokeAsync(StateHasChanged);

    }
}
