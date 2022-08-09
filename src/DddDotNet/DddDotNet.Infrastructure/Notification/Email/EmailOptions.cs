using DddDotNet.Infrastructure.Notification.Email.SendGrid;
using DddDotNet.Infrastructure.Notification.Email.Smtp;

namespace DddDotNet.Infrastructure.Notification.Email
{
    public class EmailOptions
    {
        public string Provider { get; set; }

        public SmtpOptions SmtpClient { get; set; }

        public SendGridOptions SendGrid { get; set; }

        public bool UsedFake()
        {
            return Provider == "Fake";
        }

        public bool UsedSmtpClient()
        {
            return Provider == "SmtpClient";
        }

        public bool UsedSendGrid()
        {
            return Provider == "SendGrid";
        }
    }
}
