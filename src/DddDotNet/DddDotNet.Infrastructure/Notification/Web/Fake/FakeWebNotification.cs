using System.Threading;
using System.Threading.Tasks;

namespace DddDotNet.Infrastructure.Notification.Web.Fake;

public class FakeWebNotification<T> : IWebNotification<T>
{
    public Task SendAsync(T message, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
