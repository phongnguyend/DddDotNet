using DddDotNet.Domain.Infrastructure.MessageBrokers;
using DddDotNet.Infrastructure.MessageBrokers.RabbitMQ;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Xunit;

namespace DddDotNet.IntegrationTests.Infrastructure.MessageBrokers
{
    public class RabbitMQSenderTests
    {
        private static RabbitMQSenderOptions _rabbitMQSenderOptions;

        public RabbitMQSenderTests()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddUserSecrets("09f024f8-e8d1-4b78-9ddd-da941692e8fa")
                .Build();

            _rabbitMQSenderOptions = new RabbitMQSenderOptions();

            config.GetSection("MessageBroker:RabbitMQ").Bind(_rabbitMQSenderOptions);
        }

        [Fact]
        public async Task SendAsync_Success()
        {
            for (int i = 0; i < 10; i++)
            {
                var message = Message.GetTestMessage();
                var metaData = new MetaData { };
                var sender = new RabbitMQSender<Message>(_rabbitMQSenderOptions);
                await sender.SendAsync(message, metaData);
            }
        }
    }
}
