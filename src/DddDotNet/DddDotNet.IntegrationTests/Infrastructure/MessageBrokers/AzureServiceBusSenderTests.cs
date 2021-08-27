using DddDotNet.Domain.Infrastructure.MessageBrokers;
using DddDotNet.Infrastructure.MessageBrokers.AzureServiceBus;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Xunit;

namespace DddDotNet.IntegrationTests.Infrastructure.MessageBrokers
{
    public class AzureServiceBusSenderTests
    {
        private static string _connectionString;

        public AzureServiceBusSenderTests()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddUserSecrets("09f024f8-e8d1-4b78-9ddd-da941692e8fa")
                .Build();

            _connectionString = config["MessageBroker:AzureServiceBus:ConnectionString"];
        }

        [Fact]
        public async Task SendAsync_Success()
        {
            for (int i = 0; i < 10; i++)
            {
                var message = Message.GetTestMessage();
                var metaData = new MetaData { };
                var sender = new AzureServiceBusSender<Message>(_connectionString, "integration-test");
                await sender.SendAsync(message, metaData);
            }
        }
    }
}
