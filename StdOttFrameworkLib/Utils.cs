using System;
using System.Windows.Threading;

namespace StdOttFramework
{
    public static class Utils
    {
        public static void InvokeSafe(Delegate method, params object[] args)
        {
            Dispatcher.CurrentDispatcher.Invoke(method, args);
        }

        public static TResult InvokeSafe<TResult>(Func<TResult> func)
        {
            return Dispatcher.CurrentDispatcher.Invoke(func);
        }

    }
}
