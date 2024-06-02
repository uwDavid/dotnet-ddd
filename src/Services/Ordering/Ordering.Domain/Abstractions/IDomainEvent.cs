using MediatR;

namespace Ordering.Domain.Abstractions;

public interface IDomainEvent : INotification
{
    // use MediatR so that Domain Events can be dispatched via MediatR handlers
    Guid EventId => Guid.NewGuid();
    public DateTime OccurredOn => DateTime.Now;
    public string EventType => GetType().AssemblyQualifiedName;
}