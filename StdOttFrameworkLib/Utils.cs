using System;
using System.IO;
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

        public static string GetFullPath(string fileName)
        {
            return Path.Combine(Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]), fileName);
        }
    }
}
