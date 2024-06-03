using Ordering.Domain.Abstractions;
using Ordering.Domain.Models;

namespace Ordering.Domain.Events;

// IDomainEvent interface extends INotification
public record OrderCreatedEvent(Order order) : IDomainEvent;