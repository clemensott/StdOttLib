using System.Collections.Generic;
using System.Linq;

namespace StdOttStandard.Linq
{
    public static class KeyValuePairsUtils
    {
        public static IEnumerable<(TKey key, TValue value)> ToTuples<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> pairs)
        {
            return pairs.Select(p => (p.Key, p.Value));
        }

        public static IEnumerable<(int index, TKey key, TValue value)> ToTuplesWithIndex<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> pairs)
        {
            int index = 0;
            return pairs.Select(p => (index++, p.Key, p.Value));
        }

        public static IEnumerable<KeyValuePair<TKey, TValue>> CreatePairs<TKey, TValue>(TKey key, TValue value)
        {
            yield return new KeyValuePair<TKey, TValue>(key, value);
        }

        public static IEnumerable<KeyValuePair<TKey, TValue>> CreatePairs<TKey, TValue>(TKey key1, TValue value1, TKey key2, TValue value2)
        {
            yield return new KeyValuePair<TKey, TValue>(key1, value1);
            yield return new KeyValuePair<TKey, TValue>(key2, value2);
        }

        public static IEnumerable<KeyValuePair<TKey, TValue>> CreatePairs<TKey, TValue>(TKey key1, TValue value1,
            TKey key2, TValue value2, TKey key3, TValue value3)
        {
            yield return new KeyValuePair<TKey, TValue>(key1, value1);
            yield return new KeyValuePair<TKey, TValue>(key2, value2);
            yield return new KeyValuePair<TKey, TValue>(key3, value3);
        }

        public static IEnumerable<KeyValuePair<TKey, TValue>> CreatePairs<TKey, TValue>(TKey key1, TValue value1,
            TKey key2, TValue value2, TKey key3, TValue value3, TKey key4, TValue value4)
        {
            yield return new KeyValuePair<TKey, TValue>(key1, value1);
            yield return new KeyValuePair<TKey, TValue>(key2, value2);
            yield return new KeyValuePair<TKey, TValue>(key3, value3);
            yield return new KeyValuePair<TKey, TValue>(key4, value4);
        }

        public static IEnumerable<KeyValuePair<TKey, TValue>> CreatePairs<TKey, TValue>(TKey key1, TValue value1,
            TKey key2, TValue value2, TKey key3, TValue value3, TKey key4, TValue value4, TKey key5, TValue value5)
        {
            yield return new KeyValuePair<TKey, TValue>(key1, value1);
            yield return new KeyValuePair<TKey, TValue>(key2, value2);
            yield return new KeyValuePair<TKey, TValue>(key3, value3);
            yield return new KeyValuePair<TKey, TValue>(key4, value4);
            yield return new KeyValuePair<TKey, TValue>(key5, value5);
        }
    }
}
