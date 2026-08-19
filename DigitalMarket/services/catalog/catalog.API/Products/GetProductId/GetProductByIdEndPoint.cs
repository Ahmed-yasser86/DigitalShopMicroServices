using catalog.API.Models;

namespace catalog.API.Products.GetProductId
{

    public record GetProductByIdRespones(Product Product);
    public class GetProductByIdEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
            {

                var result = await sender.Send(new GetProductByIdQuery(id));
                var respones = result.Adapt<GetProductByIdRespones>();
                return Results.Ok(respones);


            }).WithName("GetProductById")
        .Produces<GetProductByIdRespones>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Product By Id")
        .WithDescription("Get Product By Id"); ;
        }
    }
}
