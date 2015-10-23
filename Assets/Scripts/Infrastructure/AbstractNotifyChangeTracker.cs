using System.Collections.Generic;

public abstract class AbstractNotifyChangeTracker
{
    public abstract void NotifyCollectionWillBeCleared<T>(IEnumerable<T> trackableCollection);
    public abstract void NotifyValueAdded<T>(IEnumerable<T> trackableCollection, T added);
    public abstract void NotifyValueChanged<T>(IEnumerable<T> trackableValue);
    public abstract void NotifyValueWillBeRemoved<T>(IEnumerable<T> trackableCollection, T removed);
}
