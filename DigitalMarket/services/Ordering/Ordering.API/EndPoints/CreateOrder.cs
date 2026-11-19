using Carter;
using Mapster;
using MediatR;
using Ordering.Application.Dtos;
using Ordering.Application.Orders.Commands.CreateOrders;

namespace Ordering.API.EndPoints
{



    public record createOrderRequest(OrderDto orderDto);

    public record createOrderResponese(Guid Id);
    public class CreateOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {

            app.MapPost("/orders",async (createOrderRequest request,ISender Send) =>
            {

                var command = request.Adapt<CreateOrderCommand>();

                var Result = await Send.Send(command);

                var response = Result.Adapt<createOrderResponese>();

                return Results.Created($"orders/{response.Id}", response);
            
            
            }).WithName("CreateOrder")
        .Produces<createOrderResponese>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Order")
        .WithDescription("Create Order"); ;

        }
    }
}
