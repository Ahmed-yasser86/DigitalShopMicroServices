
namespace catalog.API.Products.UpdateProduct
{


    public record UpdateProductRequest(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price);
    public record UpdateProductResponse(bool IsSuccess);
    public class UpdateProductByIdEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products", async (UpdateProductRequest request, ISender send) =>
            {

                var command = request.Adapt<UpdateProductCommand>();

                var sender = send.Send(command);

                var result = sender.Adapt<UpdateProductResponse>();

                return Results.Ok(result);
            }).WithName("UpdateProduct")
            .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Update Product")
            .WithDescription("Update Product"); ;
        }
    }
}
