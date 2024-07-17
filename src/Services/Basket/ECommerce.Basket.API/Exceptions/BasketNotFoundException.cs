﻿namespace ECommerce.Basket.API.Exceptions;
public class BasketNotFoundException : NotFoundException
{
    public BasketNotFoundException(string userName) : base(userName)
    {
    }
}