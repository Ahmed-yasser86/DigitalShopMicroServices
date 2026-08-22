

using Basket.API.Data;
using Basket.API.Models;
using BuildingBlocks.CQRS;

namespace Basket.API.Basket.GetBasket
{

    public record GetBasketQuery(string username) : IQuery<GetBasketResult>;
    public record GetBasketResult(ShoppingCart ShoppingCart);
    public class GetBasketQueryHandler(IBasketRepository basketRepository) : IqueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            // to do get basked from db
            // var basket = repositry.getbasekt(request.username);
            var basket = await basketRepository.GetBasket(request.username,cancellationToken);
            return new GetBasketResult(basket);
        }
    }
}
