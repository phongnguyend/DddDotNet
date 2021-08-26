using DddDotNet.Domain.Infrastructure.MessageBrokers;
using DddDotNet.Infrastructure.MessageBrokers.AzureEventHub;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Xunit;

namespace DddDotNet.IntegrationTests.Infrastructure.MessageBrokers
{
    public class AzureEventHubSenderTests
    {
        private static string _connectionString;

        public AzureEventHubSenderTests()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddUserSecrets("09f024f8-e8d1-4b78-9ddd-da941692e8fa")
                .Build();

            _connectionString = config["MessageBroker:AzureEventHub:ConnectionString"];
        }

        [Fact]
        public async Task SendAsync_Success()
        {
            var message = new { Id = Guid.NewGuid() };
            var metaData = new MetaData { };
            var azureQueueSender = new AzureEventHubSender<object>(_connectionString, "integration-test");
            await azureQueueSender.SendAsync(message, metaData);
        }
    }
}
