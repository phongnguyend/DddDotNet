using DddDotNet.Domain.Infrastructure.MessageBrokers;
using Google.Cloud.PubSub.V1;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace DddDotNet.Infrastructure.MessageBrokers.GooglePubSub
{
    public class GooglePubSubReceiver<T> : IMessageReceiver<T>
    {
        private readonly GooglePubSubOptions _options;

        public GooglePubSubReceiver(GooglePubSubOptions options)
        {
            _options = options;
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", _options.CredentialFilePath);
        }

        public void Receive(Action<T, MetaData> action)
        {
            Task.Factory.StartNew(() => ReceiveAsync(action));
        }

        private Task ReceiveAsync(Action<T, MetaData> action)
        {
            return ReceiveStringAsync(retrievedMessage =>
            {
                var message = JsonSerializer.Deserialize<Message<T>>(retrievedMessage);
                action(message.Data, message.MetaData);
            });
        }

        public void ReceiveString(Action<string> action)
        {
            Task.Factory.StartNew(() => ReceiveStringAsync(action));
        }

        private async Task ReceiveStringAsync(Action<string> action)
        {
            SubscriptionName subscriptionName = new SubscriptionName(_options.ProjectId, _options.SubscriptionId);
            SubscriberClient subscriber = await SubscriberClient.CreateAsync(subscriptionName);
            await subscriber.StartAsync((msg, cancellationToken) =>
            {
                action(msg.Data.ToStringUtf8());
                return Task.FromResult(SubscriberClient.Reply.Ack);
            });
            await subscriber.StopAsync(TimeSpan.FromSeconds(15));
        }
    }
}
