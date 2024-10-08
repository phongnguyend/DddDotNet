using DddDotNet.Domain.Entities;
using System;

namespace DddDotNet.Domain.Events;

public class EntityDeletedEvent<T> : IDomainEvent
    where T : Entity<Guid>
{
    public EntityDeletedEvent(T entity, DateTime eventDateTime)
    {
        Entity = entity;
        EventDateTime = eventDateTime;
    }

    public T Entity { get; }

    public DateTime EventDateTime { get; }
}
