using BuildingBlocks.CQRS;
using catalog.API.Models;
using Marten;
using Marten.Linq.QueryHandlers;

namespace catalog.API.Products.GetProductByCategory
{


    public record GetProductbyCategoryQuery(string Category) : IQuery<GetProductbyCategoryResult>;
    public record GetProductbyCategoryResult(IEnumerable<Product> Products);
    public class GetProductByCategoryHandler(IDocumentSession session)
        : IqueryHandler<GetProductbyCategoryQuery, GetProductbyCategoryResult>
    {
        public async Task<GetProductbyCategoryResult> Handle(GetProductbyCategoryQuery query, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>().Where(item => item.Category.Contains(query.Category)).ToListAsync();

            return new GetProductbyCategoryResult(products);
        }
    }
}
