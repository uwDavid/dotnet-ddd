namespace Ordering.Domain.Abstractions;

public abstract class Aggregate<TId> : Entity<TId>, IAggregate<TId>
{
    // private mutable list
    private readonly List<IDomainEvent> _domainEvents = new();

    // expose list as readonly
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    // functions
    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public IDomainEvent[] ClearDomainEvents()
    {
        IDomainEvent[] dequedEvents = _domainEvents.ToArray();
        _domainEvents.Clear();
        return dequedEvents;
    }
}