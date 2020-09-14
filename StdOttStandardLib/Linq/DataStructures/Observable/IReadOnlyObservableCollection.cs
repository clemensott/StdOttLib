using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace StdOttStandard.Linq.DataStructures.Observable
{
    public interface IReadOnlyObservableCollection<T> : IEnumerable<T>, IReadOnlyCollection<T>, IReadOnlyList<T>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        event EventHandler<SingleChangeEventArgs<T>> Added;
        event EventHandler<SingleChangeEventArgs<T>> Removed;
        event EventHandler<SetItemEventArgs<T>> Set;
        event EventHandler<CollectionClearEventArgs<T>> Cleared;
        event EventHandler<MoveItemEventArgs<T>> Moved;

        event EventHandler<SingleChangeEventArgs<T>> AddedAny;
        event EventHandler<SingleChangeEventArgs<T>> RemovedAny;

        event EventHandler<ItemPropertyChangedEventArgs<T>> ItemPropertyChanged;

        void AddItemPropertyChangedEventHandler(string propertyName, EventHandler<ItemPropertyChangedEventArgs<T>> eventHandler);

        void RemoveItemPropertyChangedEventHandler(string propertyName, EventHandler<ItemPropertyChangedEventArgs<T>> eventHandler);
    }
}
