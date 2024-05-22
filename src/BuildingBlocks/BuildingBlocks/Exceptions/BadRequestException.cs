namespace BuildingBlocks.Exceptions;

public class BadRequestException : Exception
{
    public string? Details { get; }

    public BadRequestException(string msg) : base(msg)
    {
    }

    public BadRequestException(string msg, string details) : base(msg)
    {
        Details = details;
    }
}