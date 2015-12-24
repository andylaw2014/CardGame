using System;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.Outdate.Infrastructure.Trackable
{
    public abstract class TrackableCollection<T> : IEnumerable<T>
    {
        // Avoid null EventHandler
        public EventHandler<TrackableCollectionChangeEventArgs<T>> AddedEventHandler = delegate { };
        public EventHandler<TrackableCollectionChangeEventArgs<T>> ClearEventHandler = delegate { };
        public EventHandler<TrackableCollectionChangeEventArgs<T>> InsertedEventHandler = delegate { };
        public EventHandler<TrackableCollectionChangeEventArgs<T>> RemovedEventHandler = delegate { };

        public abstract IEnumerator<T> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            if (AddToCollection(item))
                AddedEventHandler(this, new TrackableCollectionChangeEventArgs<T>(item));
        }

        public void Remove(T item)
        {
            if (RemoveFromCollection(item))
                RemovedEventHandler(this, new TrackableCollectionChangeEventArgs<T>(item));
        }

        public void Insert(int index, T item)
        {
            if (InsertToCollection(index, item))
                InsertedEventHandler(this, new TrackableCollectionChangeEventArgs<T>(item, index));
        }

        public void Clear()
        {
            ClearCollection();
            ClearEventHandler(this, new TrackableCollectionChangeEventArgs<T>(default(T)));
        }

        public void AddWithoutTracking(T item)
        {
            AddToCollection(item);
        }

        public void RemoveWithoutTracking(T item)
        {
            RemoveFromCollection(item);
        }

        public void InsertWithoutTracking(T item, int index)
        {
            InsertToCollection(index, item);
        }

        public void CleartWithoutTracking()
        {
            ClearCollection();
        }

        protected abstract bool AddToCollection(T item); // Return true if add to collection successfully
        protected abstract bool RemoveFromCollection(T item); // Return true if remove from collection successfully
        protected abstract bool InsertToCollection(int index, T item);
        // // Return true if insert to collection successfully
        protected abstract void ClearCollection();
    }
}