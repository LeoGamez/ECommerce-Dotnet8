namespace ECommerce.Basket.API.Models;
public class ShoppingCartItem
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S4581:\"new Guid()\" should not be used", Justification = "<Pending>")]
    public Guid ProductId { get; set; } = default!;
    public string ProductName { get; set; } = default!;
    public int Quantity { get; set; } = default!;
    public string Color { get; set; } = default!;
    public decimal Price { get; set; } = default!;
}
