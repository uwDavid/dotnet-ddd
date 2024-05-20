
using Marten.Pagination;

namespace Catalog.API.Products.GetProducts;

// 1 - Define Query + Result type
public record GetProductsQuery(int? PageNumber = 1, int? PageSize = 10)
    : IQuery<GetProdcutsResult>;
public record GetProdcutsResult(IEnumerable<Product> Products);


public class GetProductsQueryHandler(
    IDocumentSession session)  // Marten session DI
    : IQueryHandler<GetProductsQuery, GetProdcutsResult>
{

    // logic for handler
    // we define Query + Result types
    // implement logic based on Query input

    public async Task<GetProdcutsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        // logger.LogInformation("GetProductsQueryHandler.Handle called with {@Query}", query);
        // var products = await session.Query<Product>().ToListAsync(cancellationToken);
        var products = await session.Query<Product>()
            .ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 10, cancellationToken);
        return new GetProdcutsResult(products);
    }
}