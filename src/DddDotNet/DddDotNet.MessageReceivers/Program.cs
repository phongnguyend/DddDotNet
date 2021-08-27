using DddDotNet.Infrastructure.MessageBrokers.AzureEventHub;
using DddDotNet.Infrastructure.MessageBrokers.AzureQueue;
using DddDotNet.Infrastructure.MessageBrokers.AzureServiceBus;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Text;

namespace DddDotNet.MessageReceivers
{
    class Message
    {
        public string Id { get; set; }

        public string Text1 { get; set; }

        public string Text2 { get; set; }

        public DateTime DateTime1 { get; set; }

        public DateTime DateTime2 { get; set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"Id: {Id}");
            stringBuilder.AppendLine($"Text1: {Text1}");
            stringBuilder.AppendLine($"Text2: {Text2}");
            stringBuilder.AppendLine($"DateTime1: {DateTime1}");
            stringBuilder.AppendLine($"DateTime2: {DateTime2}");
            return stringBuilder.ToString();
        }
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
                Console.WriteLine($"AzureQueue: {message}");
            });

            var azureServiceBus = new AzureServiceBusReceiver<Message>(
                config["MessageBroker:AzureServiceBus:ConnectionString"],
                "integration-test");
            azureServiceBus.Receive((message, metaData) =>
            {
                Console.WriteLine($"AzureServiceBus: {message}");
            });

            var azureServiceBusSubscription = new AzureServiceBusSubscriptionReceiver<Message>(
                config["MessageBroker:AzureServiceBus:ConnectionString"],
                "topic-integration-test",
                "sub-integration-test");
            azureServiceBusSubscription.Receive((message, metaData) =>
            {
                Console.WriteLine($"AzureServiceBusSubscription: {message}");
            });

            var azureEventHub = new AzureEventHubReceiver<Message>(
                config["MessageBroker:AzureEventHub:ConnectionString"],
                "integration-test",
                config["MessageBroker:AzureEventHub:StorageConnectionString"],
                "eventhub-integration-test");
            azureEventHub.Receive((message, metaData) =>
            {
                Console.WriteLine($"AzureEventHub: {message}");
            });

            var azureQueueEventGrid = new AzureQueueReceiver<EventGridEvent>(
                config["MessageBroker:AzureQueue:ConnectionString"],
                "event-grid-integration-test");
            azureQueueEventGrid.ReceiveString((message) =>
            {
                var eventGridEvent = JsonConvert.DeserializeObject<EventGridEvent>(message);
                Console.WriteLine($"AzureQueueEventGridSubscription: {message}");
                Console.WriteLine($"AzureQueueEventGridSubscription: {eventGridEvent.Data}");
            });

            Console.ReadLine();
        }
    }
}
