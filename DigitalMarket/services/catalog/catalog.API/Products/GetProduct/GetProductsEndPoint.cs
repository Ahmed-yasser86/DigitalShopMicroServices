
using catalog.API.Models;

namespace catalog.API.Products.GetProduct
{

    public record GetProductsRespones(IEnumerable<Product> Products);
    public class GetProductsEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.Map("/Products", async (ISender sender) => {

                var Result = await sender.Send(new GetProductsQuery());
                var repones = Result.Adapt<GetProductsRespones>();
                return Results.Ok(repones);
            }).WithName("GetProducts")
        .Produces<GetProductsRespones>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products")
        .WithDescription("Get Products"); ;
        }
    }
}
