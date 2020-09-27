using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace StdOttStandard.Linq
{
    public static class EnumerableUtils
    {
        public static void ForEach<TSource>(this IEnumerable<TSource> src, Action<TSource> action)
        {
            foreach (TSource item in src)
            {
                action(item);
            }
        }

        public static bool BothNullOrSequenceEqual<T>(this IEnumerable<T> enum1, IEnumerable<T> enum2)
        {
            if (ReferenceEquals(enum1, enum2)) return true;
            if (enum1 == null || enum2 == null) return false;

            return enum1.SequenceEqual(enum2);
        }

        public static bool BothNullOrSequenceEqual<T>(this IEnumerable<T> enum1, IEnumerable<T> enum2,
            IEqualityComparer<T> comparer)
        {
            if (ReferenceEquals(enum1, enum2)) return true;
            if (enum1 == null || enum2 == null) return false;

            return enum1.SequenceEqual(enum2, comparer);
        }

        public static string Join(this IEnumerable<string> src, string separator = "\r\n")
        {
            return string.Join(separator, src);
        }

        public static string Join<T>(this IEnumerable<T> src, string separator = "\r\n")
        {
            return string.Join(separator, src);
        }

        public static int IndexOf(this IEnumerable enumerable, object searchItem)
        {
            if (enumerable is IList list) return list.IndexOf(searchItem);

            int i = 0;

            foreach (object item in enumerable)
            {
                if (Equals(item, searchItem)) return i;

                i++;
            }

            return -1;
        }

        public static int IndexOf<TSource>(this IEnumerable<TSource> enumerable, TSource searchItem)
        {
            if (enumerable is IList<TSource> list) return list.IndexOf(searchItem);

            int i = 0;
            IEqualityComparer<TSource> comparer = EqualityComparer<TSource>.Default;

            foreach (TSource item in enumerable)
            {
                if (comparer.Equals(item, searchItem)) return i;

                i++;
            }

            return -1;
        }

        public static int IndexOf<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            int i = 0;

            foreach (T item in enumerable)
            {
                if (predicate(item)) return i;

                i++;
            }

            return -1;
        }

        public static T ElementAtCycle<T>(this ICollection<T> source, int index)
        {
            index = StdUtils.CycleIndex(index, source.Count);

            if (source is IList<T> list) return list[index];

            return source.ElementAt(index);
        }

        public static T ElementAtCycle<T>(this IEnumerable<T> source, int index, int count)
        {
            index = StdUtils.CycleIndex(index, count);

            if (source is IList<T> list) return list[index];

            return source.ElementAt(index);
        }

        public static TSource MinElement<TSource>(this IEnumerable<TSource> src, Func<TSource, IComparable> selector)
        {
            TSource minItem;
            IEnumerable<TSource> remaining = src.Extract(out minItem);
            IComparable minValue = selector(minItem);

            foreach (TSource item in remaining)
            {
                IComparable value = selector(item);

                if (value?.CompareTo(minValue) != -1) continue;

                minItem = item;
                minValue = value;
            }

            return minItem;
        }

        public static TSource MaxElement<TSource>(this IEnumerable<TSource> src, Func<TSource, IComparable> selector)
        {
            TSource maxItem;
            IEnumerable<TSource> remaining = src.Extract(out maxItem);
            IComparable maxValue = selector(maxItem);

            foreach (TSource item in remaining)
            {
                IComparable value = selector(item);

                if (value?.CompareTo(maxValue) != 1) continue;

                maxItem = item;
                maxValue = value;
            }

            return maxItem;
        }

        public static void Swap<T>(this IList<T> items, int index1, int index2)
        {
            T tmp = items[index1];
            items[index1] = items[index2];
            items[index2] = tmp;
        }

        public static IEnumerable<(int index, T item)> WithIndex<T>(this IEnumerable<T> source)
        {
            int index = 0;
            return source.Select(item => (index++, item));
        }

        public static IEnumerable<object> Cast(this IEnumerable src)
        {
            return src.Cast<object>();
        }

        public static IEnumerable<T> ReverseAsIList<T>(this IEnumerable<T> src)
        {
            IList<T> array = src.ToIList();

            for (int i = array.Count - 1; i >= 0; i--)
            {
                yield return array[i];
            }
        }

        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key) where TValue : new()
        {
            TValue value;
            if (!dict.TryGetValue(key, out value))
            {
                value = new TValue();
                dict.Add(key, value);
            }

            return value;
        }

        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, Action<TValue> initAction) where TValue : new()
        {
            TValue value;
            if (!dict.TryGetValue(key, out value))
            {
                value = new TValue();
                initAction?.Invoke(value);
                dict.Add(key, value);
            }

            return value;
        }

        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, Func<TValue> createFunc)
        {
            TValue value;
            if (!dict.TryGetValue(key, out value))
            {
                value = createFunc();
                dict.Add(key, value);
            }

            return value;
        }

        public static int LeastCommonDenominator(IEnumerable<int> src)
        {
            int lcd;
            foreach (int no in src.Extract(out lcd))
            {
                lcd = StdUtils.LeastCommonDenominator(lcd, no);
            }

            return lcd;
        }

        public static int GreatestCommonDivisor(IEnumerable<int> src)
        {
            int gcd;
            foreach (int no in src.Extract(out gcd))
            {
                gcd = StdUtils.GreatestCommonDivisor(gcd, no);
            }

            return gcd;
        }
    }
}
