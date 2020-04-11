using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace StdOttStandard.Linq
{
    public static class MutateUtils
    {
        public static void RemoveLast(this IList list, int count = 1)
        {
            for (int i = 0; i < count && list.Count > 0; i++)
            {
                list.RemoveAt(list.Count - 1);
            }
        }

        public static void RemoveLast<T>(this IList<T> list, int count = 1)
        {
            for (int i = 0; i < count && list.Count > 0; i++)
            {
                list.RemoveAt(list.Count - 1);
            }
        }

        public static void RemoveLastWhile(this IList list, Func<object, bool> predicate)
        {
            while (list.Count > 0 && predicate(list[list.Count - 1]))
            {
                list.RemoveAt(list.Count - 1);
            }
        }

        public static void RemoveLastWhile<T>(this IList<T> list, Func<T, bool> predicate)
        {
            while (list.Count > 0 && predicate(list[list.Count - 1]))
            {
                list.RemoveAt(list.Count - 1);
            }
        }
    }
}
