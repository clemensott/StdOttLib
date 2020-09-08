using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace StdOttUwp
{
    public static class UwpUtils
    {
        public static async Task RunSafe(this DispatchedHandler handler, CoreDispatcherPriority priority = CoreDispatcherPriority.Normal)
        {
            CoreDispatcher dispatcher = CoreApplication.MainView.CoreWindow.Dispatcher;

            if (dispatcher.HasThreadAccess) handler();
            else await dispatcher.RunAsync(priority, handler);
        }

        public static async Task<StorageFile> TryGetFileAsync(this StorageFolder folder, string name)
        {
            IStorageItem item = await folder.TryGetItemAsync(name);
            return item is StorageFile ? (StorageFile)item : null;
        }

        public static async Task<StorageFolder> TryGetFolderAsync(this StorageFolder folder, string name)
        {
            IStorageItem item = await folder.TryGetItemAsync(name);
            return item is StorageFolder ? (StorageFolder)item : null;
        }

        public static IAsyncAction ToNotNull(this IAsyncAction task)
        {
            return task ?? AsyncInfo.Run((_) => Task.CompletedTask);
        }

        public static IAsyncOperation<TResult> ToNotNull<TResult>(this IAsyncOperation<TResult> task, TResult fallbackValue = default(TResult))
        {
            return task ?? AsyncInfo.Run((_) => Task.FromResult(fallbackValue));
        }

        public static T GetDataContext<T>(object sender)
        {
            return ((FrameworkElement)sender).GetDataContext<T>();
        }

        public static T GetDataContext<T>(this FrameworkElement sender)
        {
            return (T)sender.DataContext;
        }

        public static bool TryGetDataContext<T>(object sender, out T dataContext)
        {
            return TryGetDataContext<T>(sender as FrameworkElement, out dataContext);
        }

        public static bool TryGetDataContext<T>(this FrameworkElement sender, out T dataContext)
        {
            if (sender?.DataContext is T)
            {
                dataContext = (T)sender.DataContext;
                return true;
            }

            dataContext = default(T);
            return false;
        }

        public static T GetDataContextOrDefault<T>(object sender)
        {
            return TryGetDataContext(sender, out T dataContext) ? dataContext : default(T);
        }

        public static T GetDataContextOrDefault<T>(this FrameworkElement sender)
        {
            return TryGetDataContext(sender, out T dataContext) ? dataContext : default(T);
        }
    }
}
