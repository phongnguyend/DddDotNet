namespace DddDotNet.Domain.Infrastructure.MessageBrokers
{
    public interface IMessageSender<T>
    {
        void Send(T message, MetaData metaData = null);
    }
}
