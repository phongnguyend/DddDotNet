using Azure.Messaging.EventGrid;
using Azure.Storage.Queues;
using DddDotNet.Infrastructure.MessageBrokers.AmazonSQS;
using DddDotNet.Infrastructure.MessageBrokers.ApacheActiveMQ;
using DddDotNet.Infrastructure.MessageBrokers.AzureEventHub;
using DddDotNet.Infrastructure.MessageBrokers.AzureQueue;
using DddDotNet.Infrastructure.MessageBrokers.AzureServiceBus;
using DddDotNet.Infrastructure.MessageBrokers.GooglePubSub;
using DddDotNet.Infrastructure.MessageBrokers.RabbitMQ;
using Microsoft.Extensions.Configuration;
using System;
using System.Text;
using System.Threading.Tasks;

namespace DddDotNet.MessageReceivers;

class Message
{
    public string Id { get; set; }

    public string Text1 { get; set; }

    public string Text2 { get; set; }

    public DateTime DateTime1 { get; set; }

    public DateTime DateTime2 { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine();
        stringBuilder.AppendLine($"Id: {Id}");
        stringBuilder.AppendLine($"Text1: {Text1}");
        stringBuilder.AppendLine($"Text2: {Text2}");
        stringBuilder.AppendLine($"DateTime1: {DateTime1}");
        stringBuilder.AppendLine($"DateTime2: {DateTime2}");
        stringBuilder.AppendLine($"CreatedDateTime: {CreatedDateTime}");
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

        var amazonSqsOptions = new AmazonSqsOptions();
        config.GetSection("MessageBroker:AmazonSQS").Bind(amazonSqsOptions);
        var amazonSqs = new AmazonSqsReceiver<Message>(amazonSqsOptions);
        _ = amazonSqs.ReceiveAsync(async (message, metaData) =>
        {
            Console.WriteLine($"AmazonSqs: {message}");
            await Task.CompletedTask;
        });

        var apacheActiveMqOptions = new ApacheActiveMQOptions();
        config.GetSection("MessageBroker:ApacheActiveMQ").Bind(apacheActiveMqOptions);
        var apacheActiveMq = new ApacheActiveMQReceiver<Message>(apacheActiveMqOptions);
        _ = apacheActiveMq.ReceiveAsync(async (message, metaData) =>
        {
            Console.WriteLine($"ApacheActiveMQ: {message}");
            await Task.CompletedTask;
        });

        var azureQueue = new AzureQueueReceiver<Message>(
            config["MessageBroker:AzureQueue:ConnectionString"],
            "integration-test");
        _ = azureQueue.ReceiveAsync(async (message, metaData) =>
        {
            Console.WriteLine($"AzureQueue: {message}");
            await Task.CompletedTask;
        });

        var azureServiceBusQueue = new AzureServiceBusQueueReceiver<Message>(
            config["MessageBroker:AzureServiceBus:ConnectionString"],
            "integration-test");
        _ = azureServiceBusQueue.ReceiveAsync(async (message, metaData) =>
        {
            Console.WriteLine($"AzureServiceBus: {message}");
            await Task.CompletedTask;
        });

        var azureServiceBusSubscription = new AzureServiceBusSubscriptionReceiver<Message>(
            config["MessageBroker:AzureServiceBus:ConnectionString"],
            "topic-integration-test",
            "sub-integration-test");
        _ = azureServiceBusSubscription.ReceiveAsync(async (message, metaData) =>
        {
            Console.WriteLine($"AzureServiceBusSubscription: {message}");
            await Task.CompletedTask;
        });

        var azureEventHub = new AzureEventHubReceiver<Message>(
            config["MessageBroker:AzureEventHub:ConnectionString"],
            "integration-test",
            config["MessageBroker:AzureEventHub:StorageConnectionString"],
            "eventhub-integration-test");
        _ = azureEventHub.ReceiveAsync(async (message, metaData) =>
        {
            Console.WriteLine($"AzureEventHub: {message}");
            await Task.CompletedTask;
        });

        var azureQueueEventGrid = new AzureQueueReceiver<EventGridEvent>(
            config["MessageBroker:AzureQueue:ConnectionString"],
            "event-grid-integration-test",
            QueueMessageEncoding.Base64);
        _ = azureQueueEventGrid.ReceiveStringAsync(async (message) =>
        {
            try
            {
                EventGridEvent eventGridEvent = EventGridEvent.Parse(new BinaryData(message));
                Console.WriteLine($"AzureQueueEventGridSubscription: {message}");
                Console.WriteLine($"AzureQueueEventGridSubscription: {eventGridEvent.Data}");
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        });

        var googlePubSubOptions = new GooglePubSubOptions();
        config.GetSection("MessageBroker:GooglePubSub").Bind(googlePubSubOptions);
        var googlePubSub = new GooglePubSubReceiver<Message>(googlePubSubOptions);
        _ = googlePubSub.ReceiveAsync(async (message, metaData) =>
        {
            Console.WriteLine($"GooglePubSub: {message}");
            await Task.CompletedTask;
        });

        var rabbitMQReceiverOptions = new RabbitMQReceiverOptions()
        {
            AutomaticCreateEnabled = true,
            QueueType = "quorum",
            //MessageEncryptionEnabled = true,
            MessageEncryptionKey = "KEhv7V8VedlhVlNr5vQstLk99l5uflYGB5lamGZd4R4="
        };
        config.GetSection("MessageBroker:RabbitMQ").Bind(rabbitMQReceiverOptions);
        var rabbitMqReceiver = new RabbitMQReceiver<Message>(rabbitMQReceiverOptions);
        _ = rabbitMqReceiver.ReceiveAsync(async (message, metaData) =>
        {
            Console.WriteLine($"RabbitMQ: {message}");
            await Task.CompletedTask;
        });

        Console.ReadLine();
    }
}
