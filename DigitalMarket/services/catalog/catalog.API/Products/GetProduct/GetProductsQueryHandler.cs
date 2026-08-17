using BuildingBlocks.CQRS;
using catalog.API.Models;
using Marten;
using Marten.Linq.QueryHandlers;
namespace catalog.API.Products.GetProduct
{


    public record GetProductsQuery : IQuery<GetProductsResult>;
    public record GetProductsResult(IEnumerable<Product> Products);
    public class GetProductsQueryHandler(IDocumentSession session , ILogger<GetProductsQueryHandler> logger )
        : IqueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("Get ProductQueryHandler was triggered with {@Query}", query);
            var results = await session.Query<Product>().ToListAsync(cancellationToken);


            return new GetProductsResult(results);

        }
    }

   
}
