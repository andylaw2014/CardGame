using System;

public class ValueChangedEventArgs<T> : EventArgs
{
    public T Value { get; private set; }
    public ValueChangedEventArgs(T value)
    {
        Value = value;
    }
}