using System;

namespace StdOttStandard.Linq.DataStructures.Observable
{
    public class SetItemEventArgs<T> : EventArgs
    {
        public int Index { get; }

        public T OldItem { get; }

        public T NewItem { get; }

        internal SetItemEventArgs(int index, T oldItem, T newItem)
        {
            Index = index;
            OldItem = oldItem;
            NewItem = newItem;
        }
    }
}
