
using Basket.API.Models;

namespace Basket.API.Basket.StoreBasket
{


    public record StoreBasketRequest(ShoppingCart Cart);
    public record StoreBasketResponse(string UserName);
    public class StoreBasktEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/Basekt", async (StoreBasketRequest storeBasketRequest, ISender sender) =>
            {
                var ToSend = storeBasketRequest.Adapt<StoreBasketCommand>();

                var result = await sender.Send(ToSend);

                var response = result.Adapt<StoreBasketResponse>();

                return Results.Created($"/basket/{response.UserName}", response);


            }).WithName("CreateProduct")
        .Produces<StoreBasketResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Product")
        .WithDescription("Create Product"); ;


        }
    }
}
