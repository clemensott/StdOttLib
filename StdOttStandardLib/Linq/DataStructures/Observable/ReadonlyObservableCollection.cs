using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace StdOttStandard.Linq.DataStructures.Observable
{
    public class ReadonlyObservableCollection<T> : IReadOnlyObservableCollection<T>
    {
        private readonly IReadOnlyObservableCollection<T> source;

        public event EventHandler<SingleChangeEventArgs<T>> Added
        {
            add => source.Added += value;
            remove => source.Added -= value;
        }

        public event EventHandler<SingleChangeEventArgs<T>> Removed
        {
            add => source.Removed += value;
            remove => source.Removed -= value;
        }

        public event EventHandler<SetItemEventArgs<T>> Set
        {
            add => source.Set += value;
            remove => source.Set -= value;
        }

        public event EventHandler<CollectionClearEventArgs<T>> Cleared
        {
            add => source.Cleared += value;
            remove => source.Cleared -= value;
        }

        public event EventHandler<MoveItemEventArgs<T>> Moved
        {
            add => source.Moved += value;
            remove => source.Moved -= value;
        }

        public event EventHandler<SingleChangeEventArgs<T>> AddedAny
        {
            add => source.AddedAny += value;
            remove => source.AddedAny -= value;
        }

        public event EventHandler<SingleChangeEventArgs<T>> RemovedAny
        {
            add => source.RemovedAny += value;
            remove => source.RemovedAny -= value;
        }

        public event EventHandler<ItemPropertyChangedEventArgs<T>> ItemPropertyChanged
        {
            add => source.ItemPropertyChanged += value;
            remove => source.ItemPropertyChanged -= value;
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add => source.CollectionChanged += value;
            remove => source.CollectionChanged -= value;
        }

        public event PropertyChangedEventHandler PropertyChanged
        {
            add => source.PropertyChanged += value;
            remove => source.PropertyChanged -= value;
        }

        public T this[int index] => source[index];

        public int Count => source.Count;

        public bool IsReadOnly => true;

        public ReadonlyObservableCollection(ObservableCollection<T> source)
        {
            this.source = source;
        }

        public void AddItemPropertyChangedEventHandler(string propertyName, EventHandler<ItemPropertyChangedEventArgs<T>> eventHandler)
        {
            source.AddItemPropertyChangedEventHandler(propertyName, eventHandler);
        }

        public void RemoveItemPropertyChangedEventHandler(string propertyName, EventHandler<ItemPropertyChangedEventArgs<T>> eventHandler)
        {
            source.RemoveItemPropertyChangedEventHandler(propertyName, eventHandler);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return source.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return source.GetEnumerator();
        }
    }
}
