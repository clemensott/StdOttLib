using System;

namespace StdOttStandard.CollectionSubscriber
{
    public class ChangedEventArgs<T>:EventArgs
    {
        public int Index { get; }

        public T Item { get; }

        public ChangedEventArgs(int index, T item)
        {
            Index = index;
            Item = item;
        }
    }
}