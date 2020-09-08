using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace StdOttStandard.Linq.DataStructures
{
    public class DistinctCollection<T> : ObservableCollection<T>
    {
        public DistinctCollection() : base()
        {
        }

        public DistinctCollection(IEnumerable<T> collection) : base(collection)
        {
        }

        public DistinctCollection(List<T> list) : base(list)
        {
        }

        protected override void InsertItem(int index, T item)
        {
            if (!Contains(item)) base.InsertItem(index, item);
        }

        protected override void SetItem(int index, T item)
        {
            if (!this[index].Equals(item) && Contains(item)) RemoveAt(index);
            else base.SetItem(index, item);
        }

        public void AddRange(IEnumerable<T> items)
        {
            foreach (T item in items) Add(item);
        }

        public void RemoveRange(IEnumerable<T> items)
        {
            foreach (T item in items) Remove(item);
        }
    }
}
