﻿using DddDotNet.Domain.Infrastructure.MessageBrokers;
using DddDotNet.Infrastructure.MessageBrokers.AzureServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Threading.Tasks;
using Xunit;

namespace DddDotNet.IntegrationTests.Infrastructure.MessageBrokers
{
    public class AzureServiceBusQueueSenderTests
    {
        private static string _connectionString;

        public AzureServiceBusQueueSenderTests()
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
                var sender = new AzureServiceBusQueueSender<Message>(_connectionString, "integration-test");
                await sender.SendAsync(message, metaData);
            }
        }

        [Fact]
        public async Task HealthCheck_Healthy()
        {
            var healthCheck = new AzureServiceBusQueueHealthCheck(
                connectionString: _connectionString,
                queueName: "integration-test");
            var checkResult = await healthCheck.CheckHealthAsync(new HealthCheckContext { Registration = new HealthCheckRegistration("Test", (x) => null, HealthStatus.Degraded, new string[] { }) });
            Assert.Equal(HealthStatus.Healthy, checkResult.Status);
        }

        [Fact]
        public async Task HealthCheck_Degraded()
        {
            var healthCheck = new AzureServiceBusQueueHealthCheck(
                connectionString: _connectionString,
                queueName: Guid.NewGuid().ToString());
            var checkResult = await healthCheck.CheckHealthAsync(new HealthCheckContext { Registration = new HealthCheckRegistration("Test", (x) => null, HealthStatus.Degraded, new string[] { }) });
            Assert.Equal(HealthStatus.Degraded, checkResult.Status);
        }
    }
}
