
using ECommerce.Ordering.Domain.Models;
using ECommerce.Ordering.Domain.ValueObjects;

namespace ECommerce.Ordering.Infrastructure.Data.Extensions;

internal class InitialData
{
    public static IEnumerable<Customer> Customers => new List<Customer>
    {
        Customer.Create(CustomerId.Of(new Guid("B6974628-95E7-4B7B-AD86-D8FA49F8B0BE")),"John Doe","john.doe@test.com"),
        Customer.Create(CustomerId.Of(new Guid("62F97C80-4520-4EA2-8EAE-2EE8A27EFAE7")),"Jane Smith","jane.smith@test.com"),
    };

    public static IEnumerable<Product> Products => new List<Product>
    {
        Product.Create(ProductId.Of(new Guid("B6974628-95E7-4B7B-AD86-D8FA49F8B0BE")),"Iphone 14",1200),
        Product.Create(ProductId.Of(new Guid("62F97C80-4520-4EA2-8EAE-2EE8A27EFAE7")),"Iphone 15",1500),
        Product.Create(ProductId.Of(new Guid("B01A7C3C-F425-4E26-BAD2-9379265813DF")),"Samsung Galaxy S24",1300),
        Product.Create(ProductId.Of(new Guid("F4B40D33-5564-409D-81BE-62B0F919F7AA")),"Samsung Galaxy S24 Ultra",1400),
    };

    public static IEnumerable<Order> SeedOrders()
    {
        // Sample data
        var productIds = Products.Select(p => p.Id).ToList();
        var customers = Customers.ToList();

        var payment = Payment.Of("John Doe", "4111111111111111", "12/24", "123", 1);
        var payment2 = Payment.Of("John Doe","3222222222222222", "12/29", "123", 1);
        var shippingAddress = Address.Of("John", "Doe", "john.doe@test.com", "123 Main St", "USA", "NY", "10001");
        var billingAddress = Address.Of("John", "Doe", "john.doe@test.com", "123 Main St", "USA", "NY", "10001");
        
        var shippingAddress2 = Address.Of("Jane", "Smith", "jane.smith@test.com", "456 Main St", "USA", "NY", "10001");
        var billingAddress2 = Address.Of("Jane", "Smith", "jane.smith@test.com", "456 Main St", "USA", "NY", "10001");

        var orderName1 = OrderName.Of("10001");
        var orderName2 = OrderName.Of("10002");

        // Create orders
        var order1 = Order.Create(OrderId.Of(Guid.NewGuid()), customers[0].Id, orderName1, shippingAddress, billingAddress, payment);
        var order2 = Order.Create(OrderId.Of(Guid.NewGuid()), customers[1].Id, orderName2, shippingAddress2, billingAddress2, payment2);

        // Add order items to orders
        order1.Add(productIds[0], 2, 1200m);
        order1.Add(productIds[1], 1, 1500m);

        order2.Add(productIds[2], 3, 1300m);
        order2.Add(productIds[3], 1, 1400m);

        // Return orders
        return new List<Order> { order1, order2 };
    }
}
