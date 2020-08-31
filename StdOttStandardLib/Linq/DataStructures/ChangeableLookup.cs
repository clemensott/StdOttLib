using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace StdOttStandard.Linq.DataStructures
{
    public class ChangeableLookup<TKey, TValue> : IReadOnlyDictionary<TKey, IReadOnlyList<TValue>>
    {
        private readonly IDictionary<TKey, List<TValue>> dict;
        private readonly IDictionary<TKey, IReadOnlyList<TValue>> readonlyDict;

        public IReadOnlyList<TValue> this[TKey key] => readonlyDict[key];

        public IEnumerable<TKey> Keys => readonlyDict.Keys;

        public IEnumerable<IReadOnlyList<TValue>> Values => readonlyDict.Values;

        public int Count => readonlyDict.Count;

        public ChangeableLookup()
        {
            dict = new Dictionary<TKey, List<TValue>>();
            readonlyDict = new Dictionary<TKey, IReadOnlyList<TValue>>();
        }

        public ChangeableLookup(ILookup<TKey, TValue> source):this()
        {
            foreach (IGrouping<TKey, TValue> group in source)
            {
                List<TValue> list = group.ToList();
                dict.Add(group.Key, list);
                readonlyDict.Add(group.Key, list.AsReadOnly());
            }
        }

        public void Add(TKey key, TValue value)
        {
            List<TValue> list;
            if (!dict.TryGetValue(key, out list))
            {
                list = new List<TValue>();
                dict.Add(key, list);
                readonlyDict.Add(key, list.AsReadOnly());
            }

            list.Add(value);
        }

        public bool Remove(TKey key, TValue value)
        {
            List<TValue> list;
            return !dict.TryGetValue(key, out list) && list.Remove(value);
        }

        public bool ContainsKey(TKey key)
        {
            return readonlyDict.ContainsKey(key);
        }

        public IEnumerator<KeyValuePair<TKey, IReadOnlyList<TValue>>> GetEnumerator()
        {
            return readonlyDict.GetEnumerator();
        }

        public bool TryGetValue(TKey key, out IReadOnlyList<TValue> value)
        {
            return readonlyDict.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerable<TValue> GetAllValues()
        {
            return Values.SelectMany(list => list);
        }
    }
}
