using DddDotNet.Domain.Infrastructure.MessageBrokers;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Queue;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace DddDotNet.Infrastructure.MessageBrokers.AzureQueue
{
    public class AzureQueueReceiver<T> : IMessageReceiver<T>
    {
        private readonly string _connectionString;
        private readonly string _queueName;

        public AzureQueueReceiver(string connectionString, string queueName)
        {
            _connectionString = connectionString;
            _queueName = queueName;
        }

        public void Receive(Action<T, MetaData> action)
        {
            Task.Factory.StartNew(() => ReceiveAsync(action));
        }

        private Task ReceiveAsync(Action<T, MetaData> action)
        {
            return ReceiveStringAsync(retrievedMessage =>
            {
                var message = JsonConvert.DeserializeObject<Message<T>>(retrievedMessage);
                action(message.Data, message.MetaData);
            });
        }

        public void ReceiveString(Action<string> action)
        {
            Task.Factory.StartNew(() => ReceiveStringAsync(action));
        }

        private async Task ReceiveStringAsync(Action<string> action)
        {
            var storageAccount = CloudStorageAccount.Parse(_connectionString);
            var queueClient = storageAccount.CreateCloudQueueClient();
            var queue = queueClient.GetQueueReference(_queueName);

            await queue.CreateIfNotExistsAsync();

            while (true)
            {
                var retrievedMessage = await queue.GetMessageAsync();

                if (retrievedMessage != null)
                {
                    action(retrievedMessage.AsString);
                    await queue.DeleteMessageAsync(retrievedMessage);
                }
                else
                {
                    await Task.Delay(1000);
                }
            }
        }
    }
}
