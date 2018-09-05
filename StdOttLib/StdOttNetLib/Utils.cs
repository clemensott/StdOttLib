using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace StdOttWpfLib
{
    public static class Utils
    {
        public static NotImplementedException GetNotImplementedExeption(object obj, string methodName)
        {
            return GetNotImplementedExeption(obj.GetType(), methodName);
        }

        public static NotImplementedException GetNotImplementedExeption(Type classType, string methodName)
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
    }
}
