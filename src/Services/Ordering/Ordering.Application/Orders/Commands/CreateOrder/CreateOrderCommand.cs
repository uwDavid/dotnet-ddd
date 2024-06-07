using BuildingBlocks.CQRS;

using FluentValidation;

using Ordering.Application.Dtos;

namespace Ordering.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(OrderDto Order) : ICommand<CreateOrderResult>;
// Command encapsulates the data required to create Order

public record CreateOrderResult(Guid id);
// return OrderId

// validator
public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    // performs validations in the constructor
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Order.CustomerId).NotNull().WithMessage("CustomerId is required");
        RuleFor(x => x.Order.OrderItems).NotEmpty().WithMessage("OrderItems is required");
    }
}