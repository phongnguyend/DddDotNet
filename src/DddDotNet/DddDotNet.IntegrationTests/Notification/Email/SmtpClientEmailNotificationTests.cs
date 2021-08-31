using DddDotNet.Infrastructure.Notification.Email.SmtpClient;
using System.Threading.Tasks;
using Xunit;

namespace DddDotNet.IntegrationTests.Notification.Email
{
    public class SmtpClientEmailNotificationTests
    {
        public SmtpClientEmailNotificationTests()
        {
        }

        [Fact]
        public async Task SendAsync_Success()
        {
            SmtpClientEmailNotification smtpClient = new SmtpClientEmailNotification(new SmtpClientOptions
            {
                Host = "localhost",
            });

            await smtpClient.SendAsync(new EmailMessage
            {
                From = "phong@gmail.com",
                Tos = "phong@gmail.com",
                Subject = "IntegrationTests 1",
                Body = "IntegrationTests 1",
            });

            await smtpClient.SendAsync(new EmailMessage
            {
                From = "phong@gmail.com",
                FromName = "",
                Tos = "phong@gmail.com",
                Subject = "IntegrationTests 2",
                Body = "IntegrationTests 2",
            });

            await smtpClient.SendAsync(new EmailMessage
            {
                From = "phong@gmail.com",
                FromName = "Phong Nguyen",
                Tos = "phong@gmail.com",
                Subject = "IntegrationTests 3",
                Body = "IntegrationTests 3",
            });
        }
    }
}
