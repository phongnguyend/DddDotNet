﻿using Amazon;
using Amazon.SimpleNotificationService;
using DddDotNet.Domain.Infrastructure.MessageBrokers;
using System.Threading;
using System.Threading.Tasks;

namespace DddDotNet.Infrastructure.MessageBrokers.AmazonSNS;

public class AmazonSnsSender<T> : IMessageSender<T>
{
    private readonly AmazonSnsOptions _options;

    public AmazonSnsSender(AmazonSnsOptions options)
    {
        _options = options;
    }

    public async Task SendAsync(T message, MetaData metaData = null, CancellationToken cancellationToken = default)
    {
        var snsClient = new AmazonSimpleNotificationServiceClient(_options.AccessKeyID, _options.SecretAccessKey, RegionEndpoint.GetBySystemName(_options.RegionEndpoint));

        var publishResponse = await snsClient.PublishAsync(_options.TopicARN, new Message<T>
        {
            Data = message,
            MetaData = metaData,
        }.SerializeObject());
    }
}
