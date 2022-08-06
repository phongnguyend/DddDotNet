using DddDotNet.Domain.Infrastructure.MessageBrokers;
using DddDotNet.Infrastructure.MessageBrokers.AmazonEventBridge;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Xunit;

namespace DddDotNet.IntegrationTests.Infrastructure.MessageBrokers
{
    public class AmazonEventBridgeSenderTests
    {
        private AmazonEventBridgeOptions _options;

        public AmazonEventBridgeSenderTests()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddUserSecrets("09f024f8-e8d1-4b78-9ddd-da941692e8fa")
                .Build();

            _options = new AmazonEventBridgeOptions();

            config.GetSection("MessageBroker:AmazonEventBridge").Bind(_options);
        }

        [Fact]
        public async Task SendAsync_Success()
        {
            for (int i = 0; i < 10; i++)
            {
                var message = Message.GetTestMessage();
                var metaData = new MetaData { };
                var sender = new AmazonEventBridgeSender<Message>(_options);
                await sender.SendAsync(message, metaData);
            }
        }
    }
}
