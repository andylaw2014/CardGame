using System;

namespace Infrastructure.Trackable
{
    public class ValueChangedEventArgs<T> : EventArgs
    {
        public T Value { get; private set; }

        public ValueChangedEventArgs(T value)
        {
            Value = value;
        }
    }
}