using DddDotNet.Domain.Infrastructure.MessageBrokers;
using RabbitMQ.Client;
using System.Threading;
using System.Threading.Tasks;

namespace DddDotNet.Infrastructure.MessageBrokers.RabbitMQ
{
    public class RabbitMQSender<T> : IMessageSender<T>
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly string _exchangeName;
        private readonly string _routingKey;

        public RabbitMQSender(RabbitMQSenderOptions options)
        {
            _connectionFactory = new ConnectionFactory
            {
                HostName = options.HostName,
                UserName = options.UserName,
                Password = options.Password,
            };

            _exchangeName = options.ExchangeName;
            _routingKey = options.RoutingKey;
        }

        public async Task SendAsync(T message, MetaData metaData = null, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                using var connection = _connectionFactory.CreateConnection();
                using var channel = connection.CreateModel();
                var body = new Message<T>
                {
                    Data = message,
                    MetaData = metaData,
                }.GetBytes();
                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.BasicPublish(exchange: _exchangeName,
                                     routingKey: _routingKey,
                                     basicProperties: properties,
                                     body: body);
            }, cancellationToken);
        }
    }
}
