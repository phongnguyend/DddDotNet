using Amazon;
using Amazon.SQS;
using DddDotNet.Domain.Infrastructure.MessageBrokers;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace DddDotNet.Infrastructure.MessageBrokers.AmazonSQS
{
    public class AmazonSqsReceiver<T> : IMessageReceiver<T>
    {
        private readonly AmazonSqsOptions _options;

        public AmazonSqsReceiver(AmazonSqsOptions options)
        {
            _options = options;
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
            var sqsClient = new AmazonSQSClient(_options.AccessKeyID, _options.SecretAccessKey, RegionEndpoint.GetBySystemName(_options.RegionEndpoint));

            while (true)
            {
                try
                {
                    var retrievedMessages = await sqsClient.ReceiveMessageAsync(_options.QueueUrl);

                    if (retrievedMessages.Messages.Count > 0)
                    {
                        foreach (var retrievedMessage in retrievedMessages.Messages)
                        {
                            action(retrievedMessage.Body);
                            await sqsClient.DeleteMessageAsync(_options.QueueUrl, retrievedMessage.ReceiptHandle);
                        }
                    }
                    else
                    {
                        await Task.Delay(1000);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    await Task.Delay(1000);
                }
            }
        }
    }
}
