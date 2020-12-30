using DddDotNet.Domain.Infrastructure.MessageBrokers;
using System;

namespace DddDotNet.Infrastructure.MessageBrokers.Fake
{
    public class FakeReceiver<T> : IMessageReceiver<T>
    {
        public void Receive(Action<T, MetaData> action)
        {
            // do nothing
        }
    }
}
