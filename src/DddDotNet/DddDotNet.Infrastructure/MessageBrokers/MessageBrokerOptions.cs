using DddDotNet.Infrastructure.MessageBrokers.AzureEventGrid;
using DddDotNet.Infrastructure.MessageBrokers.AzureEventHub;
using DddDotNet.Infrastructure.MessageBrokers.AzureQueue;
using DddDotNet.Infrastructure.MessageBrokers.AzureServiceBus;
using DddDotNet.Infrastructure.MessageBrokers.Kafka;
using DddDotNet.Infrastructure.MessageBrokers.RabbitMQ;

namespace DddDotNet.Infrastructure.MessageBrokers
{
    public class MessageBrokerOptions
    {
        public string Provider { get; set; }

        public RabbitMQOptions RabbitMQ { get; set; }

        public KafkaOptions Kafka { get; set; }

        public AzureQueueOptions AzureQueue { get; set; }

        public AzureServiceBusOptions AzureServiceBus { get; set; }

        public AzureEventGridOptions AzureEventGrid { get; set; }

        public AzureEventHubOptions AzureEventHub { get; set; }

        public bool UsedRabbitMQ()
        {
            return Provider == "RabbitMQ";
        }

        public bool UsedKafka()
        {
            return Provider == "Kafka";
        }

        public bool UsedAzureQueue()
        {
            return Provider == "AzureQueue";
        }

        public bool UsedAzureServiceBus()
        {
            return Provider == "AzureServiceBus";
        }

        public bool UsedAzureEventGrid()
        {
            return Provider == "AzureEventGrid";
        }

        public bool UsedAzureEventHub()
        {
            return Provider == "AzureEventHub";
        }

        public bool UsedFake()
        {
            return Provider == "Fake";
        }
    }
}
