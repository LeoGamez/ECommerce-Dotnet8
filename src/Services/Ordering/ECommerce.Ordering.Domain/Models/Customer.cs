
namespace ECommerce.Ordering.Domain.Models;

public sealed class Customer : Entity<Guid>
{
    public string Name { get; private set; } = default!;
    public string Email { get; private set; } = default!;
}
