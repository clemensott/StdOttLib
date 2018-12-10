using System;
using System.Collections.Generic;
using System.Linq;

namespace StdOttStandard
{
    public static class Utils
    {
        public static string ToString(TimeSpan span, bool includeMillis = false)
        {
            string text = string.Empty;
            int hours = (int)Math.Floor(span.TotalHours);

            if (hours >= 1) text += string.Format("{0,2}:", hours);

            text += string.Format("{0,2}:{1,2}", span.Minutes, span.Seconds);

            if (includeMillis) text += string.Format(":{0,2}", span.Milliseconds);

            return text.Replace(' ', '0');
        }

        public static string GetTypeMessageAndStack(this Exception exception)
        {
            IEnumerable<string> exceptions = ToEnumerable(exception).Select(e => e.GetType().Name + ":\r\n" + e.Message);

            return string.Join("\r\n\r\n", exceptions) + "\r\n\r\n" + exception.StackTrace;
        }

        public static IEnumerable<Exception> ToEnumerable(Exception e)
        {
            while (e != null)
            {
                yield return e;

                e = e.InnerException;
            }
        }

        public static bool BothNullOrSequenceEqual<T>(this IEnumerable<T> enum1, IEnumerable<T> enum2)
        {
            if (enum1 == enum2) return true;
            if (enum1 == null || enum2 == null) return false;

            return enum1.SequenceEqual(enum2);
        }

        public static IEnumerable<T> ToNotNull<T>(this IEnumerable<T> enumerable)
        {
            return enumerable ?? Enumerable.Empty<T>();
        }

        public static int IndexOf<T>(this IEnumerable<T> enumerable, T searchItem)
        {
            int i = 0;

            foreach (T item in enumerable)
            {
                if (ReferenzEqualOrEqual(item, searchItem)) return i;

                i++;
            }

            return -1;
        }

        public static bool ReferenzEqualOrEqual(object obj1, object obj2)
        {
            if (ReferenceEquals(obj1, obj2)) return true;
            if (!ReferenceEquals(obj1, null)) return obj1.Equals(obj2);
            if (!ReferenceEquals(obj2, null)) return obj2.Equals(obj2);

            throw new NotImplementedException();
        }

        public static IEnumerable<Type> GetBaseTypes(this Type type)
        {
            while (true)
            {
                foreach (Type baseInterface in type.GetInterfaces())
                {
                    yield return baseInterface;

                    foreach (Type baseBaseType in GetBaseTypes(baseInterface))
                    {
                        yield return baseBaseType;
                    }
                }

                type = type.BaseType;

                if (type == null) yield break;

                yield return type;
            }
        }
    }
}
