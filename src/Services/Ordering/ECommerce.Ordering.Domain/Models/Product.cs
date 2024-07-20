﻿
namespace ECommerce.Ordering.Domain.Models;

public sealed class Product : Entity<Guid>
{
    public string Name { get; private set; } = default!;
    public decimal Price { get; private set; } = default!;
}
