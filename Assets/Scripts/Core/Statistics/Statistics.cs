using System.Collections.Generic;

namespace Assets.Scripts.Core.Statistics
{
    public abstract class Statistics<T>
    {
        private readonly Dictionary<T, int> _values;

        protected Statistics()
        {
            _values = new Dictionary<T, int>();
        }


        public int this[T key]
        {
            get { return Get(key); }
            set { Set(key, value); }
        }

        public void Set(T key, int value)
        {
            if (_values.ContainsKey(key))
                _values[key] = value;
            else
                _values.Add(key, value);
        }

        public int Get(T key)
        {
            return _values[key];
        }
    }
}