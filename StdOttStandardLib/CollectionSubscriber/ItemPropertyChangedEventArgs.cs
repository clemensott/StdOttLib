using System;

namespace StdOttStandard.CollectionSubscriber
{
    public class ItemPropertyChangedEventArgs<T> : EventArgs
    {
        public T Item { get; }

        public string PropertyName { get; }

        public ItemPropertyChangedEventArgs(T item, string propertyName)
        {
            Item = item;
            PropertyName = propertyName;
        }
    }
}
