using System;

namespace Infrastructure.Trackable
{
    public class TrackableCollectionChangeEventArgs<T> : EventArgs
    {
        public T Item { get; private set; }
        public int Index { get; private set; }

        public TrackableCollectionChangeEventArgs(T item, int index = -1)
        {
            Item = item;
            Index = index;
        }
    }
}