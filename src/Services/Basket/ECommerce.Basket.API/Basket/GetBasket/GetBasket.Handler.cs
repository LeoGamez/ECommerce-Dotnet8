
namespace ECommerce.Basket.API.Basket.GetBasket;
public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;
public record GetBasketResult(ShoppingCart Cart);

internal class GetNasketQueryHandler(IBasketRepository repository) : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {
        var basket= await repository.GetShoppingCart(request.UserName, cancellationToken);
        return  new GetBasketResult(basket);
    }
}