using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Windows.Foundation.Collections;
using Windows.Storage;

namespace StdOttUwp.ApplicationDataObjects
{
    public class AppDataContainerObject : INotifyPropertyChanged
    {
        private IPropertySet values;
        protected readonly IDictionary<string, AppDataContainerObject> objects;

        public ApplicationDataContainer Container { get; private set; }

        public AppDataContainerObject()
        {
            values = new PropertySet();
            objects = new Dictionary<string, AppDataContainerObject>();
        }

        public void SetContainer(ApplicationDataContainer container, bool copyToNew)
        {
            if (container == Container) return;
            if (container == null)
            {
                RemoveContainer();
                return;
            }

            if (copyToNew && Container != null)
            {
                foreach (KeyValuePair<string, object> pair in Container.Values)
                {
                    container.Values[pair.Key] = pair.Value;
                }

                foreach (string removeKey in container.Values.Keys.Except(container.Values.Keys))
                {
                    container.Values.Remove(removeKey);
                }

                //foreach ((string key, ApplicationDataContainer value) in container.Containers.ToTuples())
                //{
                //    objects[key]
                //    obj.SetContainer(true)
                //}
            }

            Container = container;
            values = null;
        }

        public void RemoveContainer()
        {
            values = new PropertySet();

            foreach (KeyValuePair<string, object> pair in Container.Values)
            {
                values.Add(pair.Key, pair.Value);
            }

            foreach (AppDataContainerObject obj in objects.Values)
            {
                obj.RemoveContainer();
            }
        }


        protected T GetValue<T>(string propertyName)
        {
            object value;
            return GetValues().TryGetValue(propertyName, out value) ? (T)value : default(T);
        }

        protected void SetValue(string propertyName, object value)
        {
            IDictionary<string, object> currentValues = GetValues();
            if (currentValues.ContainsKey(propertyName)) currentValues[propertyName] = value;
            else currentValues.Add(propertyName, value);

            OnPropertyChanged(propertyName);
        }

        protected IDictionary<string, object> GetValues()
        {
            return Container?.Values ?? values;
        }

        protected T GetObject<T>(string propertyName) where T : AppDataContainerObject, new()
        {
            return GetObject<T>(propertyName, container =>
            {
                T newObj = new T();
                newObj.SetContainer(container, false);
                return newObj;
            });
        }

        protected T GetObject<T>(string propertyName, Func<ApplicationDataContainer, T> create) where T : AppDataContainerObject
        {
            if (objects.ContainsKey(propertyName) && Container?.Containers.ContainsKey(propertyName) != false)
            {
                return (T)objects[propertyName];
            }

            if (!objects.ContainsKey(propertyName) && Container?.Containers.ContainsKey(propertyName) != true)
            {
                return null;
            }

            if (objects.ContainsKey(propertyName))
            {
                objects.Remove(propertyName);
                return null;
            }

            ApplicationDataContainer container = Container.Containers[propertyName];
            T newObj = create(container);

            objects.Add(propertyName, newObj);
            return newObj;
        }

        protected void SetObject<T>(string propertyName, T obj) where T : AppDataContainerObject
        {
            if (objects.ContainsKey(propertyName)) objects[propertyName] = obj;
            else objects.Add(propertyName, obj);

            if (Container == null) return;

            ApplicationDataContainer container;
            if (!Container.Containers.TryGetValue(propertyName, out container))
            {
                container = Container.CreateContainer(propertyName, ApplicationDataCreateDisposition.Always);
            }

            obj.SetContainer(container, true);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
