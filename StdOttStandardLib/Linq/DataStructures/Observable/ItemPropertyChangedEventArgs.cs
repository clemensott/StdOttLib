using System;

namespace StdOttStandard.Linq.DataStructures.Observable
{
    public class ItemPropertyChangedEventArgs<T> : EventArgs
    {
        public T Item { get; }

        public string PropertyName { get; }

        internal ItemPropertyChangedEventArgs(T item, string propertyName)
        {
            Item = item;
            PropertyName = propertyName;
        }
    }
}
