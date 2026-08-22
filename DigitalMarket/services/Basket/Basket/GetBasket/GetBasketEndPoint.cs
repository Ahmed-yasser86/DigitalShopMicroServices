
using Basket.API.Models;


namespace Basket.API.Basket.GetBasket
{
    public record GetBasektResponse(ShoppingCart ShoppingCart);
    public class GetBasketEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/baseket/{userName}", async (string userName, ISender sender) => {

                var result = await sender.Send(new GetBasketQuery(userName));

                var responese = result.Adapt<GetBasektResponse>();

                return Results.Ok(responese);
            }).WithName("GetUserBasket")
        .Produces<GetBasektResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get User Basket ")
        .WithDescription("Get User Basket"); ;
        }
    }
}
