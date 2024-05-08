
namespace Catalog.API.Products.GetProducts;

// 1 - Define Request + Response type
// public record GetProductsRequest(); 
public record GetProductsResponse(IEnumerable<Product> Products);

// flow for entire endpoint 
// 1 - get client request, map to IQuery 
// 2 - use Mediator.Send() the query
// 3 - map result to client response
public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender) =>
        {
            var result = await sender.Send(new GetProductsQuery());
            // use Mapster to map Query Result to Response
            var response = result.Adapt<GetProductsResponse>();
            return Results.Ok(response);
        })
        .WithName("GetProducts")
        .Produces<GetProductsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products")
        .WithDescription("Get Products");
    }
}