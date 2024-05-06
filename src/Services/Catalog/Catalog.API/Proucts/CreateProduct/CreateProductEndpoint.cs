namespace Catalog.API.Products.CreateProduct;

// Define Request + Response types
public record CreateProductRequest(
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price
);
public record CreateProductResponse(Guid Id);

// use Carter to expose HTTP Endpoints
// Create Product - POST /products
public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        // Note - we need to map Request obj to Command obj
        // 1 - Define HTTP Post endpoint using Carter
        // 2 - Map request to command obj
        // 3 - Send command to MediatR
        // 4 - Map result to response

        app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
        {
            // use Mapster.Adapt() to map request to command obj
            var command = request.Adapt<CreateProductCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<CreateProductResponse>();

            return Results.Created($"/products/{response.Id}", response);
        })
        .WithName("CreateProduct")
        .Produces<CreateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Product")
        .WithDescription("Create Product");
    }
}
