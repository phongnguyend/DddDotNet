using DddDotNet.Domain.Infrastructure.MessageBrokers;
using DddDotNet.Infrastructure.MessageBrokers.AmazonSNS;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Xunit;

namespace DddDotNet.IntegrationTests.Infrastructure.MessageBrokers
{
    public class AmazonSnsSenderTests
    {
        private AmazonSnsOptions _options;

        public AmazonSnsSenderTests()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddUserSecrets("09f024f8-e8d1-4b78-9ddd-da941692e8fa")
                .Build();

            _options = new AmazonSnsOptions();

            config.GetSection("MessageBroker:AmazonSNS").Bind(_options);
        }

        [Fact]
        public async Task SendAsync_Success()
        {
            for (int i = 0; i < 10; i++)
            {
                var message = Message.GetTestMessage();
                var metaData = new MetaData { };
                var sender = new AmazonSnsSender<Message>(_options);
                await sender.SendAsync(message, metaData);
            }
        }
    }
}
