using System;
using System.Threading;
using System.Threading.Tasks;

namespace DddDotNet.Domain.Infrastructure.MessageBrokers;

public interface IMessageReceiver<T>
{
    Task ReceiveAsync(Func<T, MetaData, Task> action, CancellationToken cancellationToken = default);
}
