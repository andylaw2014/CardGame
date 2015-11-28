using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Assets.Scripts.Infrastructure.EventAggregator
{
    public class EventAggregator : IEventAggregator
    {
        private static readonly Action<object, object> HandlerResultProcessing = (target, result) => { };

        private readonly List<WeakEventHandler> _handlers;

        public EventAggregator()
        {
            _handlers = new List<WeakEventHandler>();
        }

        public bool HandlerExistsFor(Type messageType)
        {
            lock (_handlers)
            {
                return _handlers.Any(handler => handler.Handles(messageType) & !handler.IsDead);
            }
        }

        public void Subscribe(object subscriber)
        {
            if (subscriber == null)
                throw new ArgumentNullException("subscriber");

            lock (_handlers)
            {
                if (!_handlers.Any(x => x.Matches(subscriber)))
                {
                    _handlers.Add(new WeakEventHandler(subscriber));
                }
            }
        }

        public void Publish(object message)
        {
            Log.Verbose(message);
            Publish(message, action => action());
        }

        private void Publish(object message, Action<Action> marshal)
        {
            if (message == null)
                throw new ArgumentNullException("message");

            if (marshal == null)
                throw new ArgumentNullException("marshal");

            WeakEventHandler[] toNotify;
            lock (_handlers)
            {
                toNotify = _handlers.ToArray();
            }

            marshal(() =>
            {
                var messageType = message.GetType();

                var dead = toNotify
                    .Where(handler => !handler.Handle(messageType, message))
                    .ToList();

                if (!dead.Any()) return;
                lock (_handlers)
                {
                    foreach (var handler in dead)
                    {
                        _handlers.Remove(handler);
                    }
                }
            });
        }

        private class WeakEventHandler
        {
            private readonly Dictionary<Type, MethodInfo> _supportedHandlers;
            private readonly WeakReference _weakReference;

            public WeakEventHandler(object handler)
            {
                _weakReference = new WeakReference(handler);
                _supportedHandlers = new Dictionary<Type, MethodInfo>();

                var interfaces = handler.GetType().GetInterfaces()
                    .Where(x => typeof (IHandle).IsAssignableFrom(x) && x.IsGenericType);

                foreach (var @interface in interfaces)
                {
                    var type = @interface.GetGenericArguments()[0];
                    var method = @interface.GetMethod("Handle");
                    _supportedHandlers[type] = method;
                }
            }

            public bool IsDead
            {
                get { return _weakReference.Target == null; }
            }

            public bool Matches(object instance)
            {
                return _weakReference.Target == instance;
            }

            public bool Handle(Type messageType, object message)
            {
                var target = _weakReference.Target;
                if (target == null)
                {
                    return false;
                }

                foreach (var pair in _supportedHandlers)
                {
                    if (!pair.Key.IsAssignableFrom(messageType)) continue;
                    var result = pair.Value.Invoke(target, new[] {message});
                    if (result != null)
                    {
                        HandlerResultProcessing(target, result);
                    }
                }

                return true;
            }

            public bool Handles(Type messageType)
            {
                return _supportedHandlers.Any(pair => pair.Key.IsAssignableFrom(messageType));
            }
        }
    }
}