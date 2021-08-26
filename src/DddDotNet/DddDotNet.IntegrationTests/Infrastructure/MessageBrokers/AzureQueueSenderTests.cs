using DddDotNet.Domain.Infrastructure.MessageBrokers;
using DddDotNet.Infrastructure.MessageBrokers.AzureQueue;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Xunit;

namespace DddDotNet.IntegrationTests.Infrastructure.MessageBrokers
{
    public class AzureQueueSenderTests
    {
        private static string _connectionString;

        public AzureQueueSenderTests()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddUserSecrets("09f024f8-e8d1-4b78-9ddd-da941692e8fa")
                .Build();

            _connectionString = config["MessageBroker:AzureQueue:ConnectionString"];
        }

        [Fact]
        public async Task SendAsync_Success()
        {
            var message = new Message { Id = Guid.NewGuid() };
            var metaData = new MetaData { };
            var sender = new AzureQueueSender<Message>(_connectionString, "integration-test");
            await sender.SendAsync(message, metaData);
        }
    }
}
