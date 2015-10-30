using System;

namespace Infrastructure.EventAggregator
{
    public interface IEventAggregator
    {
        bool HandlerExistsFor(Type messageType);
        void Subscribe(object subscriber);
        void Publish(object message);
    }
}