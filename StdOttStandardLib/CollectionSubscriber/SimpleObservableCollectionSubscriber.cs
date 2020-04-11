using StdOttStandard.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace StdOttStandard.CollectionSubscriber
{
    public class SimpleObservableCollectionSubscriber<T>
    {
        private readonly IDictionary<T, int> dict;

        public event EventHandler<ChangedEventArgs<T>> Added;
        public event EventHandler<ChangedEventArgs<T>> Removed;
        public event EventHandler<ItemPropertyChangedEventArgs<T>> ItemPropertyChanged;

        public ObservableCollection<T> Collection { get; }

        public SimpleObservableCollectionSubscriber(ObservableCollection<T> collection)
        {
            dict = new Dictionary<T, int>();

            Collection = collection;
            Collection.CollectionChanged += Collection_CollectionChanged;

            foreach ((int index, T item) in Collection.WithIndex())
            {
                Add(index, item);
            }
        }

        private void Collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    OnAdd(e);
                    break;

                case NotifyCollectionChangedAction.Move:
                    OnMove(e);
                    break;

                case NotifyCollectionChangedAction.Remove:
                    OnRemove(e);
                    break;

                case NotifyCollectionChangedAction.Replace:
                    OnReplace(e);
                    break;

                case NotifyCollectionChangedAction.Reset:
                    OnReset(e);
                    break;
            }
        }

        private void OnAdd(NotifyCollectionChangedEventArgs e)
        {
            for (int i = 0; i < e.NewItems.Count; i++)
            {
                Add(e.NewStartingIndex + i, (T)e.NewItems[i]);
            }
        }

        private void Add(int index, T item)
        {
            if (ReferenceEquals(item, null)) return;

            dict[item] = index;

            INotifyPropertyChanged changedItem = item as INotifyPropertyChanged;
            if (changedItem != null) changedItem.PropertyChanged += Item_PropertyChanged;

            Added?.Invoke(this, new ChangedEventArgs<T>(index, item));
        }

        private void OnRemove(NotifyCollectionChangedEventArgs e)
        {
            for (int i = 0; i < e.OldItems.Count; i++)
            {
                Remove(e.OldStartingIndex + i, (T)e.OldItems[i]);
            }
        }

        private void Remove(int index, T item)
        {
            if (ReferenceEquals(item, null)) return;

            dict.Remove(item);

            INotifyPropertyChanged changedItem = item as INotifyPropertyChanged;
            if (changedItem != null) changedItem.PropertyChanged -= Item_PropertyChanged;

            Removed?.Invoke(this, new ChangedEventArgs<T>(index, item));
        }

        private void OnMove(NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems.Count != 1 || e.OldItems.Count != 1)
            {
                throw new NotImplementedException("Moving more than one item at a time is not implemented");
            }

            int lowerIndex, upperIndex, offset = e.NewStartingIndex > e.OldStartingIndex ? 1 : -1;
            if (e.NewStartingIndex > e.OldStartingIndex)
            {
                lowerIndex = e.OldStartingIndex;
                upperIndex = e.NewStartingIndex;
                offset = -1;
            }
            else
            {
                lowerIndex = e.NewStartingIndex;
                upperIndex = e.OldStartingIndex;
                offset = 1;
            }

            for (int i = lowerIndex; i <= upperIndex; i++)
            {
                dict[Collection[i]] += offset;
            }
        }

        private void OnReplace(NotifyCollectionChangedEventArgs e)
        {
            for (int i = 0; i < e.NewItems.Count; i++)
            {
                Add(e.NewStartingIndex + i, (T)e.NewItems[i]);
                Remove(e.OldStartingIndex + i, (T)e.OldItems[i]);
            }
        }

        private void OnReset(NotifyCollectionChangedEventArgs e)
        {
            foreach ((T key, int value) in dict.ToTuples().ToArray())
            {
                Remove(value, key);
            }
        }

        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            T item = (T)sender;
            ItemPropertyChanged?.Invoke(this, new ItemPropertyChangedEventArgs<T>(item, e.PropertyName));
        }
    }
}