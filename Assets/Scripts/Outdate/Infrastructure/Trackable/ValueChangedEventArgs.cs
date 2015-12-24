using System;

namespace Assets.Scripts.Outdate.Infrastructure.Trackable
{
    public class ValueChangedEventArgs<T> : EventArgs
    {
        public ValueChangedEventArgs(T value)
        {
            Value = value;
        }

        public T Value { get; private set; }
    }
}