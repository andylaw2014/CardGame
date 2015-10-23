using System;

public class Trackable<T> : AbstractTrackableValue<T>
{
    public EventHandler<ValueChangedEventArgs<T>> ValueChanged;
    public override T Value
    {
        get
        {
            return _value;
        }
        set
        {
            _value = value;
            ValueChanged(this,new ValueChangedEventArgs<T>(_value));
        }
    }

    private T _value;

    public Trackable() : this(default(T))
    {
    }

    public Trackable(T value)
    {
        _value = value;
    }
    
    public static implicit operator T(Trackable<T> trackable)
    {
        return trackable.Value;
    }
}
