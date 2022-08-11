using DddDotNet.Infrastructure.Notification.Sms;

namespace DddDotNet.IntegrationTests.Notification.Sms
{
    public class SmsMessage : ISmsMessage
    {
        public string Message { get; set; }

        public string PhoneNumber { get; set; }
    }
}
