
using catalog.API.Models;
using MediatR;

namespace catalog.API.Products.GetProduct
{
    public record GetProductsRequest(int? PageNumber = 1, int? PageSize = 10);
    public record GetProductsRespones(IEnumerable<Product> Products);
    public class GetProductsEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.Map("/Products", async ([AsParameters] GetProductsRequest request, ISender sender) => {

                var query = request.Adapt<GetProductsQuery>();
                var Result = await sender.Send(query);
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
