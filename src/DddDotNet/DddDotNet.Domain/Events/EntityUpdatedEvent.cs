using DddDotNet.Domain.Entities;
using System;

namespace DddDotNet.Domain.Events;

public class EntityUpdatedEvent<T> : IDomainEvent
    where T : Entity<Guid>
{
    public EntityUpdatedEvent(T entity, DateTime eventDateTime)
    {
        Entity = entity;
        EventDateTime = eventDateTime;
    }

    public T Entity { get; }

    public DateTime EventDateTime { get; }
}
