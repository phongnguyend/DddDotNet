using System.Threading;
using System.Threading.Tasks;

namespace DddDotNet.Domain.Infrastructure.MessageBrokers
{
    public interface IMessageSender<T>
    {
        void Send(T message, MetaData metaData = null);

        Task SendAsync(T message, MetaData metaData = null, CancellationToken cancellationToken = default);
    }
}
