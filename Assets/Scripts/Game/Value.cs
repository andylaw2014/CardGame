using System.Collections.Generic;

public class Value<T>
{
    private readonly Dictionary<T, int> _resource;

    public Value()
    {
        _resource = new Dictionary<T, int> ();
    }

    public int this[T i]
    {
        get { return _resource[i]; }
        set { _resource[i] = value; }
    }
}
