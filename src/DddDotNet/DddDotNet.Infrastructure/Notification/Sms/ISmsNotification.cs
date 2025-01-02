using System.Threading;
using System.Threading.Tasks;

namespace DddDotNet.Infrastructure.Notification.Sms;

public interface ISmsNotification
{
    Task SendAsync(ISmsMessage smsMessage, CancellationToken cancellationToken = default);
}

public interface ISmsMessage
{
    public string Message { get; set; }

    public string PhoneNumber { get; set; }
}

public class SmsMessage : ISmsMessage
{
    public string Message { get; set; }

    public string PhoneNumber { get; set; }
}
