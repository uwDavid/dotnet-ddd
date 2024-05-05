using MediatR;

namespace Catalog.API.Products.CreateProduct;

// Define command + result type
public record CreateProductCommand(
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price
) : IRequest<CreateProductResult>;
// represents data we need to pass into command to create Product
// matches Product.cs

public record CreateProductResult(
    Guid Id
);
// represents response type


internal class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
{
    public Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        // business logic to create a Product
        throw new NotImplementedException();
    }
}