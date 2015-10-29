using System.Collections.Generic;

namespace Infrastructure.Trackable
{
    public class TrackableList<T> : TrackableCollection<T>
    {
        private readonly List<T> _list;

        public TrackableList()
        {
            _list = new List<T>();
        }

        public override IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        protected override bool AddToCollection(T item)
        {
            _list.Add(item);
            return true;
        }

        protected override bool RemoveFromCollection(T item)
        {
            return _list.Remove(item);
        }

        protected override bool InsertToCollection(int index, T item)
        {
            _list.Insert(index, item);
            return true;
        }

        protected override void ClearCollection()
        {
            _list.Clear();
        }
    }
}
