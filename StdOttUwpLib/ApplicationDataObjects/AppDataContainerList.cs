using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Windows.Storage;
using StdOttStandard;

namespace StdOttUwp.ApplicationDataObjects
{
    public class AppDataContainerList<T> : AppDataContainerEnumerable<T>, ICollection<T>
        where T : AppDataContainerObject, new()
    {
        public int Count => GetListKeyValuePairs().Count();

        public bool IsReadOnly => false;

        public AppDataContainerList()
        {
        }

        public void Add(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            string key = GetAddKey();

            if (Container != null)
            {
                ApplicationDataContainer container = Container.CreateContainer(key, ApplicationDataCreateDisposition.Always);

                item.SetContainer(container, false);
            }

            objects.Add(key, item);
        }

        private string GetAddKey()
        {
            string key;
            return Container.Containers.Keys.Where(k => long.TryParse(k, out _)).OrderByDescending(k => k).TryFirst(out key) ?
                GetNextKey(key) : "0";
        }

        public void Insert(int index, T item)
        {
            if (index <= 0) throw new ArgumentOutOfRangeException(nameof(index));
            if (item == null) throw new ArgumentNullException(nameof(item));

            string key = GetInsertKey(index);

            if (Container != null)
            {
                ApplicationDataContainer container = Container.CreateContainer(key, ApplicationDataCreateDisposition.Always);

                item.SetContainer(container, false);
            }

            objects.Add(key, item);
        }

        private string GetInsertKey(int index)
        {
            string beginKey, endKey;
            IEnumerable<string> keys = Container.Containers.Keys.Where(k => long.TryParse(k, out _)).OrderBy(k => k);

            return keys.Skip(index).ExtractOrDefault(out beginKey).TryFirst(out endKey) ?
                GetKeyBetween(beginKey, endKey) : "0";
        }

        private static string GetNextKey(string key)
        {
            if (key.All(c => c == '9')) return key + "1";

            long number = long.Parse(key) + 1;
            return number.ToString();
        }

        private static string GetKeyBetween(string beginKey, string endKey)
        {
            string key;

            do
            {
                key = GetNextKey(beginKey);
                beginKey += "0";
            } while (comparer.Compare(key, endKey) >= 0);

            return key;
        }

        public void Clear()
        {
            if (Container != null)
            {
                foreach (string containersKey in Container.Containers.Keys.Where(k => long.TryParse(k, out _)))
                {
                    Container.DeleteContainer(containersKey);
                }
            }

            foreach (string containersKey in objects.Keys.Where(k => long.TryParse(k, out _)))
            {
                objects.Remove(containersKey);
            }
        }

        public bool Contains(T item)
        {
            return GetListKeyValuePairs().Select(p => p.Value).Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            this.ToArray().CopyTo(array, arrayIndex);
        }

        public int IndexOf(T item)
        {
            return EnumerableUtils.IndexOf(this, item);
        }

        public bool Remove(T item)
        {
            string removeKey;
            if (!GetOrderedKeyValuePairs().Where(pair => pair.Value == item)
                .Select(pair => pair.Key).TryFirst(out removeKey)) return false;

            objects.Remove(removeKey);
            return true;
        }

        public void RemoveAt(int index)
        {
            string removeKey = GetOrderedKeyValuePairs().ElementAt(index).Key;
            objects.Remove(removeKey);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return GetOrderedKeyValuePairs().Select(p => p.Value).Cast<T>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
