
using BuildingBlocks.CQRS;
using catalog.API.Models;
using Marten;
namespace catalog.API.Products.CreateProduct
{


    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price) : Icommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);
    public class CreateProductHandler(IDocumentSession Session) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Category = request.Category,
                Description = request.Description,
                ImageFile = request.ImageFile,
                Price = request.Price
            };

            Session.Store(product);
            await Session.SaveChangesAsync();
            return new CreateProductResult(product.Id);

        }


    }
}
