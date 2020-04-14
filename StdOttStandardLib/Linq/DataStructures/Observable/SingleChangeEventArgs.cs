using System;

namespace StdOttStandard.Linq.DataStructures.Observable
{
    public class SingleChangeEventArgs<T> : EventArgs
    {
        public int Index { get; }

        public T Item { get; }

        public SingleChangeType Type { get; }

        internal SingleChangeEventArgs(int index, T item, SingleChangeType type)
        {
            Index = index;
            Item = item;
            Type = type;
        }
    }
}
