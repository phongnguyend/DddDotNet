namespace DddDotNet.Domain.Events
{
    public interface IDomainEvents
    {
        void Dispatch(IDomainEvent domainEvent);
    }
}
