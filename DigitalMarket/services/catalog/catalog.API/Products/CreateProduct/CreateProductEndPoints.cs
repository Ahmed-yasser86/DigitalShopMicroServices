using BuildingBlocks.CQRS;
using Carter;
using Mapster;
using MediatR;

namespace catalog.API.Products.CreateProduct
{


    public record CreateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price) ;
    public record CreateProductRespones(Guid Id);
    public class CreateProductEndPoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/Products", async (CreateProductRequest request, ISender sender) =>
            {

                var command = request.Adapt<CreateProductCommand>();
                var result = await sender.Send(command);
                var respones = result.Adapt<CreateProductRespones>();

                return Results.Created($"/product/{respones.Id}", respones);
            }).WithName("CreateProduct")
            .Produces<CreateProductRespones>(StatusCodes.Status201Created).
            ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Craete Product")
            .WithDescription("Create Product");
        }
    }
}
