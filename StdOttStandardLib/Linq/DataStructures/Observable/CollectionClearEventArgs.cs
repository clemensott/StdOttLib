using System;

namespace StdOttStandard.Linq.DataStructures.Observable
{
    public class CollectionClearEventArgs<T> : EventArgs
    {
        public T[] Items { get; }

        internal CollectionClearEventArgs(T[] items)
        {
            Items = items;
        }
    }
}
