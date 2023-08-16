using System.Threading;
using System.Threading.Tasks;

namespace DddDotNet.Infrastructure.Notification.Email.Fake;

public class FakeEmailNotification : IEmailNotification
{
    public Task SendAsync(IEmailMessage emailMessage, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
