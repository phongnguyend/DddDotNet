using Amazon;
using Amazon.Kinesis;
using Amazon.Kinesis.Model;
using DddDotNet.Domain.Infrastructure.MessageBrokers;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DddDotNet.Infrastructure.MessageBrokers.AmazonKinesis
{
    public class AmazonKinesisSender<T> : IMessageSender<T>
    {
        private readonly AmazonKinesisOptions _options;

        public AmazonKinesisSender(AmazonKinesisOptions options)
        {
            _options = options;
        }

        public async Task SendAsync(T message, MetaData metaData = null, CancellationToken cancellationToken = default)
        {
            var kinesisClient = new AmazonKinesisClient(_options.AccessKeyID, _options.SecretAccessKey, RegionEndpoint.GetBySystemName(_options.RegionEndpoint));

            var putRecordReponse = await kinesisClient.PutRecordAsync(new PutRecordRequest
            {
                StreamName = _options.StreamName,
                Data = new MemoryStream(Encoding.UTF8.GetBytes(new Message<T>
                {
                    Data = message,
                    MetaData = metaData,
                }.SerializeObject())),
                PartitionKey = $"PartitionKey-{Guid.NewGuid()}",
            });
        }
    }
}
