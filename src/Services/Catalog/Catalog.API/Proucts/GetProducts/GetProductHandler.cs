
namespace Catalog.API.Products.GetProducts;

// 1 - Define Query + Result type
public record GetProductsQuery() : IQuery<GetProdcutsResult>;
public record GetProdcutsResult(IEnumerable<Product> Products);


public class GetProductsQueryHandler(
    IDocumentSession session,  // Marten session DI
    ILogger<GetProductsQueryHandler> logger)
    : IQueryHandler<GetProductsQuery, GetProdcutsResult>
{

    // logic for handler
    // we define Query + Result types
    // implement logic based on Query input

    public async Task<GetProdcutsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductsQueryHandler.Handle called with {@Query}", query);
        var products = await session.Query<Product>().ToListAsync(cancellationToken);
        return new GetProdcutsResult(products);
    }
}