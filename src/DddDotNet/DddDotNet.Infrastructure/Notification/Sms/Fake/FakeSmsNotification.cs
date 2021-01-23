using System.Threading;
using System.Threading.Tasks;

namespace DddDotNet.Infrastructure.Notification.Sms.Fake
{
    public class FakeSmsNotification : ISmsNotification
    {
        public Task SendAsync(ISmsMessage smsMessage, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}
