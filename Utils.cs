using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;

namespace StdOttWpfLib
{
    public static class Utils
    {
        public static NotImplementedException GetNotImplementedExeption(this object obj, string methodName)
        {
            return GetNotImplementedExeption(obj.GetType(), methodName);
        }

        public static NotImplementedException GetNotImplementedExeption(this Type classType, string methodName)
        {
            return new NotImplementedException(classType.FullName + "." + methodName + " is not implemented");
        }

        public static void InvokeSafe(Delegate method, params object[] args)
        {
            Dispatcher.CurrentDispatcher.Invoke(method, args);
        }

        public static TResult InvokeSafe<TResult>(Func<TResult> func)
        {
            return Dispatcher.CurrentDispatcher.Invoke(func);
        }

        public static string Convert(TimeSpan span, bool includeMillis = false)
        {
            string text = string.Empty;
            int hours = (int)Math.Floor(span.TotalHours);

            if (hours >= 1) text += string.Format("{0,2}:", hours);

            text += string.Format("{0,2}:{1,2}", span.Minutes, span.Seconds);

            if (includeMillis) text += string.Format(":{0,2}", span.Milliseconds);

            return text.Replace(' ', '0');
        }

        public static string Convert(Exception exception)
        {
            return string.Join("\r\n", ToEnumerable(exception).Select(e => e.GetType().Name + ":\r\n" + e.Message));
        }

        public static IEnumerable<Exception> ToEnumerable(Exception e)
        {
            while (e != null)
            {
                yield return e;

                e = e.InnerException;
            }
        }

        public static bool NullOrSequenceEqual<T>(this IEnumerable<T> enum1, IEnumerable<T> enum2)
        {
            if (enum1 == enum2) return true;
            if (enum1 == null || enum2 == null) return false;

            return enum1.SequenceEqual(enum2);
        }

        public static IEnumerable<T> ToNotNull<T>(this IEnumerable<T> enumerable)
        {
            return enumerable ?? Enumerable.Empty<T>();
        }
    }
}
