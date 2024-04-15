namespace DddDotNet.Infrastructure.MessageBrokers.Kafka;

public class KafkaReceiverOptions
{
    public string BootstrapServers { get; set; }

    public string Topic { get; set; }

    public string GroupId { get; set; }

    public bool? AutoCommitEnabled { get; set; }
}
