namespace DddDotNet.Infrastructure.Notification.Email.SmtpClient
{
    public class FakeEmailNotification : IEmailNotification
    {
        public void Send(EmailMessageDTO emailMessage)
        {
            // do nothing
        }
    }
}
