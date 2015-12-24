using System;

namespace Assets.Scripts.Outdate.Infrastructure.Trackable
{
    public class TrackableCollectionChangeEventArgs<T> : EventArgs
    {
        public TrackableCollectionChangeEventArgs(T item, int index = -1)
        {
            Item = item;
            Index = index;
        }

        public T Item { get; private set; }
        public int Index { get; private set; }
    }
}