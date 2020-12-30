using DddDotNet.Infrastructure.Notification.Email;
using DddDotNet.Infrastructure.Notification.Sms;
using DddDotNet.Infrastructure.Notification.Web;

namespace DddDotNet.Infrastructure.Notification
{
    public class NotificationOptions
    {
        public EmailOptions Email { get; set; }

        public SmsOptions Sms { get; set; }

        public WebOptions Web { get; set; }
    }
}
