using DddDotNet.Domain.Infrastructure.MessageBrokers;
using DddDotNet.Infrastructure.MessageBrokers.ApacheActiveMQ;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Xunit;

namespace DddDotNet.IntegrationTests.Infrastructure.MessageBrokers
{
    public class ApacheActiveMQSenderTests
    {
        private ApacheActiveMQOptions _options;

        public ApacheActiveMQSenderTests()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddUserSecrets("09f024f8-e8d1-4b78-9ddd-da941692e8fa")
                .Build();

            _options = new ApacheActiveMQOptions();

            config.GetSection("MessageBroker:ApacheActiveMQ").Bind(_options);
        }

        [Fact]
        public async Task SendAsync_Success()
        {
            for (int i = 0; i < 10; i++)
            {
                var message = Message.GetTestMessage();
                var metaData = new MetaData { };
                var sender = new ApacheActiveMQSender<Message>(_options);
                await sender.SendAsync(message, metaData);
            }
        }
    }
}
