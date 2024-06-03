namespace Ordering.Domain.ValueObjects;

public record CustomerId
{
    public Guid Value { get; }

    // To implement .Of() factory method
    private CustomerId(Guid value) => Value = value;

    public static CustomerId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);

        if (value == Guid.Empty)
        {
            throw new DomainException("CustomerId cannot be empty.");
        }

        return new CustomerId(value);
    }
}