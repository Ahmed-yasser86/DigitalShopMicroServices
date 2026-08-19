using BuildingBlocks.CQRS;
using catalog.API.Models;
using Marten;
using Marten.Linq.QueryHandlers;
using Marten.Pagination;
namespace catalog.API.Products.GetProduct
{

    public record GetProductsQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetProductsResult>;
    public record GetProductsResult(IEnumerable<Product> Products);
    public class GetProductsQueryHandler(IDocumentSession session)
        : IqueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var results = await session.Query<Product>().ToPagedListAsync(query.PageNumber??0,query.PageSize??10,cancellationToken);


            return new GetProductsResult(results);

        }
    }

   
}
