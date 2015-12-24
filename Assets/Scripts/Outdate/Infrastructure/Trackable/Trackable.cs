using System;

namespace Assets.Scripts.Outdate.Infrastructure.Trackable
{
    public class Trackable<T>
    {
        private T _value;
        // Avoid null EventHandler
        public EventHandler<ValueChangedEventArgs<T>> ValueChanged = delegate { };

        public Trackable(T value = default(T))
        {
            _value = value;
        }

        public T Value
        {
            get { return _value; }
            set
            {
                if (value.Equals(_value)) return;
                _value = value;
                ValueChanged(this, new ValueChangedEventArgs<T>(_value));
            }
        }

        public override bool Equals(object obj)
        {
            // If parameter is null return false.
            return obj != null && Equals(obj as Trackable<T>);
        }

        public virtual bool Equals(Trackable<T> obj)
        {
            // If parameter is null return false:
            return obj != null && Value.Equals(obj.Value); // Return true if the fields match
        }

        public override int GetHashCode()
        {
            return (ValueChanged != null ? ValueChanged.GetHashCode() : 0) ^ Value.GetHashCode();
        }

        public static implicit operator T(Trackable<T> trackable)
        {
            return trackable.Value;
        }
    }
}