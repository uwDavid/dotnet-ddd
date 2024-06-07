namespace Ordering.Application.Dtos;

public record PaymentDto(
    string CardName,
    string CardNumber,
    string Expiration,
    string Cvv, // Mapster cannot work with all caps CVV
    int PaymentMethod
);