using DddDotNet.Domain.Infrastructure.MessageBrokers;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace DddDotNet.Infrastructure.MessageBrokers.RabbitMQ
{
    public class RabbitMQReceiver<T> : IMessageReceiver<T>, IDisposable
    {
        private readonly RabbitMQReceiverOptions _options;
        private IConnection _connection;
        private IModel _channel;
        private string _queueName;

        public RabbitMQReceiver(RabbitMQReceiverOptions options)
        {
            _options = options;

            _connection = new ConnectionFactory
            {
                HostName = options.HostName,
                UserName = options.UserName,
                Password = options.Password,
                AutomaticRecoveryEnabled = true,
            }.CreateConnection();

            _queueName = options.QueueName;

            _connection.ConnectionShutdown += Connection_ConnectionShutdown;
        }

        private void Connection_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            // TODO: add log here
        }

        public void Receive(Action<T, MetaData> action)
        {
            _channel = _connection.CreateModel();

            if (_options.AutomaticCreateEnabled)
            {
                var arguments = new Dictionary<string, object>();

                if (string.Equals(_options.QueueType, "Quorum", StringComparison.OrdinalIgnoreCase))
                {
                    arguments["x-queue-type"] = "quorum";
                }
                else if (string.Equals(_options.QueueType, "Stream", StringComparison.OrdinalIgnoreCase))
                {
                    arguments["x-queue-type"] = "stream";
                }

                if (_options.SingleActiveConsumer)
                {
                    arguments["x-single-active-consumer"] = true;
                }

                arguments = arguments.Count == 0 ? null : arguments;

                _channel.QueueDeclare(_options.QueueName, true, false, false, arguments);
                _channel.QueueBind(_options.QueueName, _options.ExchangeName, _options.RoutingKey, null);
            }

            /*In order to defeat that we can use the basicQos method with the prefetchCount = 1 setting.
             This tells RabbitMQ not to give more than one message to a worker at a time. 
             Or, in other words, don't dispatch a new message to a worker until it has processed and acknowledged the previous one. 
             Instead, it will dispatch it to the next worker that is not still busy.*/
            _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = Encoding.UTF8.GetString(ea.Body.Span);
                var message = JsonSerializer.Deserialize<Message<T>>(body);
                action(message.Data, message.MetaData);
                _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };
            _channel.BasicConsume(queue: _queueName,
                                 autoAck: false,
                                 consumer: consumer);
        }

        public void Dispose()
        {
            _channel.Dispose();
            _connection.Dispose();
        }
    }
}
