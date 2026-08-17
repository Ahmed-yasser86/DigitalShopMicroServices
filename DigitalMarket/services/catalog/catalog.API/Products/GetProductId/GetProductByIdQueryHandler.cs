using BuildingBlocks.CQRS;
using catalog.API.Exceptions;
using catalog.API.Exceptions;
using catalog.API.Models;
using Marten;
using Microsoft.Extensions.Logging;
namespace catalog.API.Products.GetProductId
{

    public record GetProductByIdQuery(Guid id) : IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product Product);
    public class GetProductByIdQueryHandler(IDocumentSession session, ILogger<GetProductByIdQueryHandler> logger) : IqueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductByIdQueryHandler was triggered with {@Query}", request);

            var result = await session.LoadAsync<Product>(request.id,cancellationToken);
            if(result is null)
            {
                throw new ProductNotFoundExcption();
            }
            return new GetProductByIdResult(result);
        }
    }
}
