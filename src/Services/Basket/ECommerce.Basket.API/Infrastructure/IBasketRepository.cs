﻿namespace ECommerce.Basket.API.Infrastructure;
public interface IBasketRepository
{
    Task<ShoppingCart> GetShoppingCart(string userName, CancellationToken cancellationToken = default);
    Task<ShoppingCart> StoreBasket(ShoppingCart cart, CancellationToken cancellationTokenToken = default);
    Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default);
}
