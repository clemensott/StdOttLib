using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace StdOttStandard
{
    public static class EnumerableUtils
    {
        public static bool BothNullOrSequenceEqual<T>(this IEnumerable<T> enum1, IEnumerable<T> enum2)
        {
            if (ReferenceEquals(enum1, enum2)) return true;
            if (enum1 == null || enum2 == null) return false;

            return enum1.SequenceEqual(enum2);
        }

        public static OnRequestBuffer<T> ToBuffer<T>(this IEnumerable<T> source)
        {
            switch (source)
            {
                case OnRequestBuffer<T> buffer:
                    return buffer;

                default:
                    return new OnRequestBuffer<T>(source);
            }
        }

        public static IEnumerable<T> ToNotNull<T>(this IEnumerable<T> enumerable)
        {
            return enumerable ?? Enumerable.Empty<T>();
        }

        public static int IndexOf(this IEnumerable enumerable, object searchItem)
        {
            if (enumerable is IList list) return list.IndexOf(searchItem);

            int i = 0;

            foreach (object item in enumerable)
            {
                if (Utils.ReferenceEqualsOrEquals(item, searchItem)) return i;

                i++;
            }

            return -1;
        }

        public static int IndexOf<T>(this IEnumerable<T> enumerable, T searchItem)
        {
            if (enumerable is IList<T> list) return list.IndexOf(searchItem);

            int i = 0;

            foreach (T item in enumerable)
            {
                if (Utils.ReferenceEqualsOrEquals(item, searchItem)) return i;

                i++;
            }

            return -1;
        }

        public static bool TryFirst<T>(this IEnumerable<T> source, out T first)
        {
            return TryFirst(source, i => true, out first);
        }

        public static bool TryFirst<T>(this IEnumerable<T> source, Func<T, bool> predicate, out T first)
        {
            foreach (T item in source)
            {
                if (!predicate(item)) continue;

                first = item;
                return true;
            }

            first = default(T);
            return false;
        }

        public static bool TryLast<T>(this IEnumerable<T> source, out T last)
        {
            return TryLast(source, out last, i => true);
        }

        public static bool TryLast<T>(this IEnumerable<T> source, out T last, Func<T, bool> predicate)
        {
            bool findElement = false;
            last = default(T);

            foreach (T item in source)
            {
                if (!predicate(item)) continue;

                last = item;
                findElement = true;
            }

            return findElement;
        }

        public static bool TrySingle<T>(this IEnumerable<T> source, out T single)
        {
            return TrySingle(source, out single, i => true);
        }

        public static bool TrySingle<T>(this IEnumerable<T> source, out T single, Func<T, bool> predicate)
        {
            bool findElement = false;
            single = default(T);

            foreach (T item in source)
            {
                if (!predicate(item)) continue;

                if (findElement)
                {
                    single = default(T);
                    return false;
                }

                single = item;
                findElement = true;
            }

            return findElement;
        }

        public static IEnumerable<T> Concat<T>(this IEnumerable<T> items, T item, params T[] more)
        {
            return Enumerable.Concat(items, Concat(item, more));
        }

        public static IEnumerable<T> Concat<T>(T first, params T[] more)
        {
            yield return first;

            foreach (T item in more) yield return item;
        }

        public static (T next, bool overflow) Next<T>(this IEnumerable<T> items, T refItem)
        {
            (T next, bool overflow, _) = OffsetElement(items, refItem, 1);

            return (next, overflow);
        }

        public static (T next, bool overflow) NextOrDefault<T>(this IEnumerable<T> items, T refItem)
        {
            (T next, bool overflow, _) = OffsetElementOrDefault(items, refItem, 1);

            return (next, overflow);
        }

        public static (T next, bool overflow) Previous<T>(this IEnumerable<T> items, T refItem)
        {
            (T next, _, bool underflow) = OffsetElement(items, refItem, -1);

            return (next, underflow);
        }

        public static (T next, bool overflow) PreviousOrDefault<T>(this IEnumerable<T> items, T refItem)
        {
            (T next, _, bool underflow) = OffsetElementOrDefault(items, refItem, -1);

            return (next, underflow);
        }

        public static (T item, bool overflow, bool underflow) OffsetElementOrDefault<T>(this IEnumerable<T> items, T refItem, int offset)
        {
            IList<T> list = items as IList<T> ?? items.ToArray();

            int refIndex = list.IndexOf(refItem);

            try
            {
                (int index, bool overflow, bool underflow) = Utils.OffsetIndex(refIndex, list.Count, offset);

                return (list[index], overflow, underflow);
            }
            catch
            {
                return (default(T), false, false);
            }
        }

        public static (T item, bool overflow, bool underflow) OffsetElement<T>(this IEnumerable<T> items, T refItem, int offset)
        {
            IList<T> list = items as IList<T> ?? items.ToArray();

            int refIndex = list.IndexOf(refItem);

            if (refIndex == -1) throw new ArgumentException("The refItem has to be in items");

            (int index, bool overflow, bool underflow) = Utils.OffsetIndex(refIndex, list.Count, offset);

            return (list[index], overflow, underflow);
        }

        public static IEnumerable<T> Remove<T>(this IEnumerable<T> items, T item, int max = 1)
        {
            if (max == -1) return RemoveAll(items, item);

            int count = 0;
            return items.Where(i =>
            {
                if (count >= max || Utils.ReferenceEqualsOrEquals(i, item)) return true;

                count++;
                return false;
            });
        }

        public static IEnumerable<T> RemoveAll<T>(this IEnumerable<T> items, T item)
        {
            return items.Where(i => !Utils.ReferenceEqualsOrEquals(i, item));
        }

        public static IEnumerable<T> Insert<T>(this IEnumerable<T> items, int index, T item)
        {
            items = ToBuffer(items);

            return items.Take(index).Concat(item).Concat(items.Skip(index));
        }

        public static TSource KeyMin<TSource>(this IEnumerable<TSource> items, Func<TSource, IComparable> selector)
        {
            using (IEnumerator<TSource> enumerator = items.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                {
                    throw new InvalidOperationException("Sequence does not contain any elements.");
                }

                TSource minItem = enumerator.Current;
                IComparable minValue = selector(minItem);

                while (enumerator.MoveNext())
                {
                    TSource item = enumerator.Current;
                    IComparable value = selector(item);

                    if (value?.CompareTo(minValue) != -1) continue;

                    minItem = item;
                    minValue = value;
                }

                return minItem;
            }
        }

        public static TSource KeyMax<TSource>(this IEnumerable<TSource> items, Func<TSource, IComparable> selector)
        {
            using (IEnumerator<TSource> enumerator = items.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                {
                    throw new InvalidOperationException("Sequence does not contain any elements.");
                }

                TSource maxItem = enumerator.Current;
                IComparable maxValue = selector(maxItem);

                while (enumerator.MoveNext())
                {
                    TSource item = enumerator.Current;
                    IComparable value = selector(item);

                    if (value?.CompareTo(maxValue) != 1) continue;

                    maxItem = item;
                    maxValue = value;
                }

                return maxItem;
            }
        }

        public static IEnumerable<T> Extract<T>(this IEnumerable<T> items, out T first)
        {
            IEnumerator<T> enumerator = items.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException("Sequence does not contain any elements.");
            }

            first = enumerator.Current;
            return ToEnumerable();

            IEnumerable<T> ToEnumerable()
            {
                while (enumerator.MoveNext())
                {
                    yield return enumerator.Current;
                }

                enumerator.Dispose();
            }
        }

        public static IEnumerable<T> ExtractOrDefault<T>(this IEnumerable<T> items, out T first)
        {
            IEnumerator<T> enumerator = items.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                enumerator.Dispose();
                first = default(T);

                return Enumerable.Empty<T>();
            }

            first = enumerator.Current;
            return ToEnumerable();

            IEnumerable<T> ToEnumerable()
            {
                while (enumerator.MoveNext())
                {
                    yield return enumerator.Current;
                }

                enumerator.Dispose();
            }
        }
    }
}
