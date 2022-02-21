using StdOttStandard;
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

        protected virtual bool TryGetValue<T>(string propertyName, out T value)
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

        protected virtual bool SetValue(string propertyName, object value)
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

        protected virtual bool TryDeserialzeObject<T>(string propertyName, out T value)
        {
            try
            {
                string xml;
                if (TryGetValue(propertyName, out xml))
                {
                    value = StdUtils.XmlDeserializeText<T>(xml);
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

        protected T DeserialzeObject<T>(string propertyName, T defaultValue = default(T))
        {
            T value;
            return TryDeserialzeObject(propertyName, out value) ? value : defaultValue;
        }

        protected virtual bool SerialzeObject(string propertyName, object value)
        {
            try
            {
                return SetValue(propertyName, StdUtils.XmlSerialize(value));
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
