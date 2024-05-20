using FluentValidation;

namespace Catalog.API.Products.CreateProduct;

// Define command + result type
public record CreateProductCommand(
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price
) : ICommand<CreateProductResult>;
// Note: represents data we need to pass into command to create Product
// matches Product.cs
// Note: we use ICommand interface defined in Buliding Blocks

public record CreateProductResult(
    Guid Id
);
// represents response type


internal class CreateProductCommandHandler(
    // IDocumentSession session, IValidator<CreateProductCommand> validator) // manual validation
    IDocumentSession session, ILogger<CreateProductCommandHandler> logger)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {

        // perform validation
        // business logic to create a Product
        // 1 - create Product instance from command obj
        // 2 - save to database
        // 3 - return result 

        // manual validation - using fluent validator
        // var result = await validator.ValidateAsync(command, cancellationToken);
        // var errors = result.Errors.Select(x => x.ErrorMessage).ToList();
        // if (errors.Any())
        // {
        //     throw new ValidationException(errors.FirstOrDefault());
        // }

        // Business Logic
        logger.LogInformation("CreateProductCommandHandler.Handle called with {@Command}", command);
        // 1 - create Product instance
        var product = new Product
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price
        };

        // 2 - save to db - skip for now
        // see primary constructor above 
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);
        // this will generate ID automatically

        // 3 - return result 
        return new CreateProductResult(product.Id);
    }
}