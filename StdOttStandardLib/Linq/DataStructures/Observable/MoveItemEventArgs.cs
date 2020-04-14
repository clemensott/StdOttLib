using System;

namespace StdOttStandard.Linq.DataStructures.Observable
{
    public class MoveItemEventArgs<T> : EventArgs
    {
        public int OldIndex { get; }

        public int NewIndex { get; }

        public T Item { get; }

        internal MoveItemEventArgs(int oldIndex, int newIndex, T item)
        {
            OldIndex = oldIndex;
            NewIndex = newIndex;
            Item = item;
        }
    }
}
