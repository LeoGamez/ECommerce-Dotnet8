
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Threading;

namespace ECommerce.Basket.API.Infrastructure;
public class CachedBasketRepository(IBasketRepository repository, IDistributedCache cache) : IBasketRepository
{
    public async Task<ShoppingCart> GetShoppingCart(string userName,CancellationToken cancellationToken = default)
    {
        var cacheBasket = await cache.GetStringAsync(userName,cancellationToken);

        if(!string.IsNullOrEmpty(cacheBasket)) 
        { 
            return JsonSerializer.Deserialize<ShoppingCart>(cacheBasket);                
        }

        var basket= await repository.GetShoppingCart(userName, cancellationToken);
        await cache.SetStringAsync(userName,JsonSerializer.Serialize(basket),cancellationToken);

        return basket;
    }

    public async Task<ShoppingCart> StoreBasket(ShoppingCart cart, CancellationToken cancellationToken = default)
    {
        await repository.StoreBasket(cart, cancellationToken);
        await cache.SetStringAsync(cart.Username, JsonSerializer.Serialize(cart), cancellationToken);

        return cart;

    }
    public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
    {

        await repository.DeleteBasket(userName, cancellationToken); 
        await cache.RemoveAsync(userName, cancellationToken);

        return true;
    }
}
