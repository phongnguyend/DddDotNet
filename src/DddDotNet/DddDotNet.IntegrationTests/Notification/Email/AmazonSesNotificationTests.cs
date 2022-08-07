using DddDotNet.Infrastructure.Notification.Email.Amazon;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Xunit;

namespace DddDotNet.IntegrationTests.Notification.Email
{
    public class AmazonSesNotificationTests
    {
        private AmazonSesOptions _options;

        public AmazonSesNotificationTests()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddUserSecrets("09f024f8-e8d1-4b78-9ddd-da941692e8fa")
                .Build();

            _options = new AmazonSesOptions();

            config.GetSection("Notification:Email:AmazonSES").Bind(_options);
        }

        [Fact]
        public async Task SendAsync_Success()
        {
            AmazonSesNotification client = new AmazonSesNotification(_options);

            await client.SendAsync(new EmailMessage
            {
                From = _options.FromEmail,
                Tos = _options.FromEmail,
                Subject = "IntegrationTests 1",
                Body = "IntegrationTests 1",
            });

            await client.SendAsync(new EmailMessage
            {
                From = _options.FromEmail,
                FromName = "",
                Tos = _options.FromEmail,
                Subject = "IntegrationTests 2",
                Body = "IntegrationTests 2",
            });

            await client.SendAsync(new EmailMessage
            {
                From = _options.FromEmail,
                FromName = "Phong Nguyen",
                Tos = _options.FromEmail,
                Subject = "IntegrationTests 3",
                Body = "IntegrationTests 3",
            });
        }
    }
}
