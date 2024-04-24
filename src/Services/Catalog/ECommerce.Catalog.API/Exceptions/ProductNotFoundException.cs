using ECommerce.Shared.Exceptions;

namespace ECommerce.Catalog.API.Exceptions;

public class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException(Guid id) : base("Product",id)
    {
    }
}