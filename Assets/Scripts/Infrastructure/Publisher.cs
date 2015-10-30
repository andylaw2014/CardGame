using System.Collections.Generic;
using System;
using System.Linq;

namespace Infrastructure
{
    public class Publisher
    {
        private readonly Dictionary<Type, List<IReceive<IMessage>>> _eventSubscribers;
        private static readonly HashSet<Type> MessageTypes= new HashSet<Type>(); 

        public Publisher()
        {
            _eventSubscribers = new Dictionary<Type, List<IReceive<IMessage>>>();
        }

        public void Publish<T>(T message) where T : IMessage
        {
            List<IReceive<IMessage>> subscribers;
            if (!_eventSubscribers.TryGetValue(typeof(T), out subscribers))
                return;
            if (subscribers.Count == 0)
                return;
            foreach (var subscriber in subscribers)
            {
                subscriber.Receive(message);
            }
        }

        public static void Register(Type type)
        {
            if (!(type.GetInterfaces().Contains(typeof(IMessage))))
                return;
            MessageTypes.Add(type);
        }

        public void Subscribe<T>(IReceive<T> instance) where T : IMessage
        {
            List<IReceive<IMessage>> subscribers;
            if (!_eventSubscribers.TryGetValue(typeof (T), out subscribers))
            {
                subscribers = new List<IReceive<T>>().Cast<IReceive<IMessage>>().ToList();
                _eventSubscribers.Add(typeof (T), subscribers);
            }
            //subscribers.Add(instance);
        }
    }
}
