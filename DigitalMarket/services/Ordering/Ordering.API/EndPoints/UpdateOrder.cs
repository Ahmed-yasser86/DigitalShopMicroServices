using Carter;
using Mapster;
using MediatR;
using Ordering.Application.Dtos;
using Ordering.Application.Orders.Commands.UpdateOrders;

namespace Ordering.API.EndPoints
{


   public record UpdateOrderRequest(OrderDto Order);

   public record updateOrderResponse(bool IsSuccess);

    public class UpdateOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {

            app.MapPut("/orders", async (UpdateOrderRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateOrderCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<updateOrderResponse>();

                return Results.Ok(response);
            })
       .WithName("UpdateOrder")
       .Produces<updateOrderResponse>(StatusCodes.Status200OK)
       .ProducesProblem(StatusCodes.Status400BadRequest)
       .WithSummary("Update Order")
       .WithDescription("Update Order");

        }
    }
}
