using System.Threading.Tasks;

namespace DddDotNet.Domain.Events
{
    public interface IDomainEventHandler<T>
           where T : IDomainEvent
    {
        Task HandleAsync(T domainEvent);
    }
}
