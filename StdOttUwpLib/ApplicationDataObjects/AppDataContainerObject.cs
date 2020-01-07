using System;
using System.Collections.Generic;
using System.ComponentModel;
using Windows.Storage;

namespace StdOttUwp.ApplicationDataObjects
{
    public class AppDataContainerObject : INotifyPropertyChanged
    {
        public ApplicationDataContainer Container { get; set; }

        public AppDataContainerObject(ApplicationDataContainer container)
        {
            Container = container ?? throw new ArgumentNullException(nameof(container));
        }

        protected bool TryGetValue<T>(string propertyName, out T value)
        {
            try
            {
                object objValue;
                bool hasValue = Container.Values.TryGetValue(propertyName, out objValue) && objValue is T;

                if (hasValue)
                {
                    value = (T)objValue;
                    return true;
                }

                value = default(T);
                return false;
            }
            catch
            {
                value = default(T);
                return false;
            }
        }

        protected T GetValue<T>(string propertyName, T defaultValue = default(T))
        {
            T value;
            return TryGetValue(propertyName, out value) ? value : defaultValue;
        }

        protected bool SetValue(string propertyName, object value)
        {
            try
            {
                IDictionary<string, object> currentValues = Container.Values;
                if (currentValues.ContainsKey(propertyName)) currentValues[propertyName] = value;
                else currentValues.Add(propertyName, value);

                OnPropertyChanged(propertyName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
