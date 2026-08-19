using BuildingBlocks.CQRS;
using catalog.API.Exceptions;
using catalog.API.Models;
using catalog.API.Products.GetProductId;
using JasperFx.Events.Daemon;
using Marten;
using Marten.Linq.QueryHandlers;

namespace catalog.API.Products.UpdateProduct
{

    public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price)
        : Icommand<UpdateProductByIdResult>;
    public record UpdateProductByIdResult(bool IsSuccess);
    public class UpdateProductByIdHandler(IDocumentSession session)
        : ICommandHandler<UpdateProductCommand, UpdateProductByIdResult>
    {

   
        public async Task<UpdateProductByIdResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
            if (product is null)
            {
                throw new ProductNotFoundExcption(command.Id);
            }

            product.Name = command.Name;
            product.Category = command.Category;
            product.Description = command.Description;
            product.ImageFile = command.ImageFile;
            product.Price = command.Price;

            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);

            return new UpdateProductByIdResult(true);
        }
    }
}
