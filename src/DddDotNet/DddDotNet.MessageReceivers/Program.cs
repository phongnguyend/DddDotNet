using DddDotNet.Infrastructure.MessageBrokers.AzureEventHub;
using DddDotNet.Infrastructure.MessageBrokers.AzureQueue;
using DddDotNet.Infrastructure.MessageBrokers.AzureServiceBus;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Extensions.Configuration;
using System;

namespace DddDotNet.MessageReceivers
{
    class Message
    {
        public string Id { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddUserSecrets("09f024f8-e8d1-4b78-9ddd-da941692e8fa")
            .Build();

            var azureQueue = new AzureQueueReceiver<Message>(
                config["MessageBroker:AzureQueue:ConnectionString"],
                "integration-test");
            azureQueue.Receive((message, metaData) =>
            {
                Console.WriteLine($"AzureQueue: {message.Id}");
            });

            var azureServiceBus = new AzureServiceBusReceiver<Message>(
                config["MessageBroker:AzureServiceBus:ConnectionString"],
                "integration-test");
            azureServiceBus.Receive((message, metaData) =>
            {
                Console.WriteLine($"AzureServiceBus: {message.Id}");
            });

            var azureServiceBusSubscription = new AzureServiceBusSubscriptionReceiver<Message>(
                config["MessageBroker:AzureServiceBus:ConnectionString"],
                "topic-integration-test",
                "sub-integration-test");
            azureServiceBusSubscription.Receive((message, metaData) =>
            {
                Console.WriteLine($"AzureServiceBusSubscription: {message.Id}");
            });

            var azureEventHub = new AzureEventHubReceiver<Message>(
                config["MessageBroker:AzureEventHub:ConnectionString"],
                "integration-test",
                config["MessageBroker:AzureEventHub:StorageConnectionString"],
                "eventhub-integration-test");
            azureEventHub.Receive((message, metaData) =>
            {
                Console.WriteLine($"AzureEventHub: {message.Id}");
            });

            var azureQueueEventGrid = new AzureQueueReceiver<EventGridEvent>(
                config["MessageBroker:AzureQueue:ConnectionString"],
                "event-grid-integration-test");
            azureQueueEventGrid.ReceiveString((message) =>
            {
                Console.WriteLine($"AzureQueueEventGridSubscription: {message}");
            });

            Console.ReadLine();
        }
    }
}
