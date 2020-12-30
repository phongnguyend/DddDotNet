using System;
using System.Collections.Generic;
using System.Text;

namespace DddDotNet.Domain.Events
{
    public interface IDomainEventHandler<T>
           where T : IDomainEvent
    {
        void Handle(T domainEvent);
    }
}
