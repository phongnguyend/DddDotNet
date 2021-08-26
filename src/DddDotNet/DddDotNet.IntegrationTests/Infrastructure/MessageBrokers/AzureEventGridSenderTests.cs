using DddDotNet.Domain.Infrastructure.MessageBrokers;
using DddDotNet.Infrastructure.MessageBrokers.AzureEventGrid;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Xunit;

namespace DddDotNet.IntegrationTests.Infrastructure.MessageBrokers
{
    public class AzureEventGridSenderTests
    {
        private static string _domainEndpoint;
        private static string _domainKey;

        public AzureEventGridSenderTests()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddUserSecrets("09f024f8-e8d1-4b78-9ddd-da941692e8fa")
                .Build();

            _domainEndpoint = config["MessageBroker:AzureEventGrid:DomainEndpoint"];
            _domainKey = config["MessageBroker:AzureEventGrid:DomainKey"];
        }

        [Fact]
        public async Task SendAsync_Success()
        {
            var message = new Message { Id = Guid.NewGuid() };
            var metaData = new MetaData { };
            var sender = new AzureEventGridSender<Message>(_domainEndpoint, _domainKey, "integration-test");
            await sender.SendAsync(message, metaData);
        }
    }
}
