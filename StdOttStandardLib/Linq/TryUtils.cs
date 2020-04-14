using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace StdOttStandard.Linq
{
    public static class TryUtils
    {
        public static bool TryIndexOf(this IEnumerable src, object searchItem, out int index)
        {
            return TryIndexOf(src.Cast(), searchItem, out index);
        }

        public static bool TryIndexOf<TSource>(this IEnumerable<TSource> src, TSource searchItem, out int index)
        {
            IEqualityComparer<TSource> comparer = EqualityComparer<TSource>.Default;
            return TryIndexOf(src, item => comparer.Equals(item, searchItem), out index);
        }

        public static bool TryIndexOf(this IEnumerable src, Func<object, bool> predicate, out int index)
        {
            return TryIndexOf(src.Cast(), predicate, out index);
        }

        public static bool TryIndexOf<TSource>(this IEnumerable<TSource> enumerable, Func<TSource, bool> predicate, out int index)
        {
            index = 0;

            foreach (TSource item in enumerable)
            {
                if (predicate(item)) return true;

                index++;
            }

            index = -1;
            return false;
        }


        public static bool TryFirst(this IEnumerable src, out object first)
        {
            return TryFirst(src.Cast(), out first);
        }

        public static bool TryFirst<TSource>(this IEnumerable<TSource> src, out TSource first)
        {
            return TryFirst(src, i => true, out first);
        }

        public static bool TryFirst(this IEnumerable src, Func<object, bool> predicate, out object first)
        {
            return TryFirst(src.Cast(), predicate, out first);
        }

        public static bool TryFirst<TSource>(this IEnumerable<TSource> src, Func<TSource, bool> predicate, out TSource first)
        {
            foreach (TSource item in src)
            {
                if (!predicate(item)) continue;

                first = item;
                return true;
            }

            first = default(TSource);
            return false;
        }


        public static bool TryLast(this IEnumerable src, out object last)
        {
            return TryLast(src.Cast(), out last);
        }

        public static bool TryLast<TSource>(this IEnumerable<TSource> src, out TSource last)
        {
            return TryLast(src, i => true, out last);
        }

        public static bool TryLast(this IEnumerable src, Func<object, bool> predicate, out object last)
        {
            return TryLast(src.Cast(), predicate, out last);
        }

        public static bool TryLast<TSource>(this IEnumerable<TSource> src, Func<TSource, bool> predicate, out TSource last)
        {
            return TryFirst(src.ReverseAsIList(), predicate, out last);
        }


        public static bool TrySingle(this IEnumerable src, out object single)
        {
            return TrySingle(src.Cast(), out single);
        }

        public static bool TrySingle<TSource>(this IEnumerable<TSource> src, out TSource single)
        {
            return TrySingle(src, i => true, out single);
        }

        public static bool TrySingle(this IEnumerable src, Func<object, bool> predicate, out object single)
        {
            return TrySingle(src.Cast(), predicate, out single);
        }

        public static bool TrySingle<TSource>(this IEnumerable<TSource> src, Func<TSource, bool> predicate, out TSource single)
        {
            bool findElement = false;
            single = default(TSource);

            foreach (TSource item in src)
            {
                if (!predicate(item)) continue;

                if (findElement)
                {
                    single = default(TSource);
                    return false;
                }

                single = item;
                findElement = true;
            }

            return findElement;
        }


        public static bool TryElementAt(this IEnumerable src, int index, out object element)
        {
            return TryElementAt(src.Cast(), index, out element);
        }

        public static bool TryElementAt<TSource>(this IEnumerable<TSource> src, int index, out TSource element)
        {
            if (index < 0)
            {
                element = default(TSource);
                return false;
            }

            int i = 0;
            foreach (TSource item in src)
            {
                if (i++ < index) continue;

                element = item;
                return true;
            }

            element = default(TSource);
            return false;
        }


        public static bool TryMinElement<TSource>(this IEnumerable<TSource> src, Func<TSource, IComparable> selector, out TSource element)
        {
            using (IEnumerator<TSource> enumerator = src.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                {
                    element = default(TSource);
                    return false;
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

                element = minItem;
                return true;
            }
        }


        public static bool TryMaxElement<TSource>(this IEnumerable<TSource> src, Func<TSource, IComparable> selector, out TSource element)
        {
            using (IEnumerator<TSource> enumerator = src.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                {
                    element = default(TSource);
                    return false;
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

                element = maxItem;
                return true;
            }
        }

        public static bool TryExtract<TSource>(this IEnumerable<TSource> items,
            out IEnumerable<TSource> remaining, out TSource first)
        {
            IEnumerable<TSource> list = items.ToBuffer();

            if (list.TryFirst(out first))
            {
                remaining = list.Skip(1);
                return true;
            }

            first = default(TSource);
            remaining = Enumerable.Empty<TSource>();
            return false;
        }
    }
}
