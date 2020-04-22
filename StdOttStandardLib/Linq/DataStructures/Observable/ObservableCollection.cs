using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace StdOttStandard.Linq.DataStructures.Observable
{
    public class ObservableCollection<T> : System.Collections.ObjectModel.ObservableCollection<T>
    {
        private readonly IDictionary<string, IList<EventHandler<ItemPropertyChangedEventArgs<T>>>> itemPropertyChangedEventHandlers;

        public event EventHandler<SingleChangeEventArgs<T>> Added;
        public event EventHandler<SingleChangeEventArgs<T>> Removed;
        public event EventHandler<SetItemEventArgs<T>> Set;
        public event EventHandler<CollectionClearEventArgs<T>> Cleared;
        public event EventHandler<MoveItemEventArgs<T>> Moved;

        public event EventHandler<SingleChangeEventArgs<T>> AddedAny;
        public event EventHandler<SingleChangeEventArgs<T>> RemovedAny;

        public event EventHandler<ItemPropertyChangedEventArgs<T>> ItemPropertyChanged;

        public ObservableCollection()
        {
            itemPropertyChangedEventHandlers = new Dictionary<string, IList<EventHandler<ItemPropertyChangedEventArgs<T>>>>();
        }

        public ObservableCollection(IEnumerable<T> collection) : base(collection)
        {
            itemPropertyChangedEventHandlers = new Dictionary<string, IList<EventHandler<ItemPropertyChangedEventArgs<T>>>>();
        }

        public void AddItemPropertyChangedEventHandler(string propertyName,
            EventHandler<ItemPropertyChangedEventArgs<T>> eventHandler)
        {
            IList<EventHandler<ItemPropertyChangedEventArgs<T>>> list;
            if (!itemPropertyChangedEventHandlers.TryGetValue(propertyName, out list))
            {
                list = new List<EventHandler<ItemPropertyChangedEventArgs<T>>>();
                itemPropertyChangedEventHandlers.Add(propertyName, list);
            }

            list.Add(eventHandler);
        }

        public void RemoveItemPropertyChangedEventHandler(string propertyName,
            EventHandler<ItemPropertyChangedEventArgs<T>> eventHandler)
        {
            IList<EventHandler<ItemPropertyChangedEventArgs<T>>> list;
            if (itemPropertyChangedEventHandlers.TryGetValue(propertyName, out list)) list.Remove(eventHandler);
        }

        protected override void ClearItems()
        {
            T[] items = new T[Count];
            for (int i = 0; i < Count; i++)
            {
                if (items[i] is INotifyPropertyChanged notify) notify.PropertyChanged -= Item_PropertyChanged;

                items[i] = this[i];
            }

            base.ClearItems();

            Cleared?.Invoke(this, new CollectionClearEventArgs<T>(items));

            for (int i = 0; i < items.Length; i++)
            {
                RemovedAny?.Invoke(this, new SingleChangeEventArgs<T>(i, items[i], SingleChangeType.Removed));
            }
        }

        protected override void InsertItem(int index, T item)
        {
            base.InsertItem(index, item);

            if (item is INotifyPropertyChanged notify) notify.PropertyChanged += Item_PropertyChanged;

            Added?.Invoke(this, new SingleChangeEventArgs<T>(index, item, SingleChangeType.Added));
            AddedAny?.Invoke(this, new SingleChangeEventArgs<T>(index, item, SingleChangeType.Added));
        }

        protected override void MoveItem(int oldIndex, int newIndex)
        {
            base.MoveItem(oldIndex, newIndex);

            Moved?.Invoke(this, new MoveItemEventArgs<T>(oldIndex, newIndex, this[newIndex]));
        }

        protected override void RemoveItem(int index)
        {
            T item = this[index];
            if (item is INotifyPropertyChanged notify) notify.PropertyChanged -= Item_PropertyChanged;

            base.RemoveItem(index);

            Removed?.Invoke(this, new SingleChangeEventArgs<T>(index, item, SingleChangeType.Removed));
            RemovedAny?.Invoke(this, new SingleChangeEventArgs<T>(index, item, SingleChangeType.Removed));
        }

        protected override void SetItem(int index, T newItem)
        {
            T oldItem = this[index];
            if (oldItem is INotifyPropertyChanged oldNotify) oldNotify.PropertyChanged -= Item_PropertyChanged;

            base.SetItem(index, newItem);
            Set?.Invoke(this, new SetItemEventArgs<T>(index, oldItem, newItem));

            if (newItem is INotifyPropertyChanged newNotify) newNotify.PropertyChanged += Item_PropertyChanged;

            RemovedAny?.Invoke(this, new SingleChangeEventArgs<T>(index, oldItem, SingleChangeType.Removed));
            AddedAny?.Invoke(this, new SingleChangeEventArgs<T>(index, newItem, SingleChangeType.Added));
        }

        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ItemPropertyChangedEventArgs<T> args = new ItemPropertyChangedEventArgs<T>((T)sender, e.PropertyName);
            ItemPropertyChanged?.Invoke(this, args);

            IList<EventHandler<ItemPropertyChangedEventArgs<T>>> list;
            if (!itemPropertyChangedEventHandlers.TryGetValue(e.PropertyName, out list)) return;

            foreach (EventHandler<ItemPropertyChangedEventArgs<T>> handler in list)
            {
                handler?.Invoke(this, args);
            }
        }
    }
}
