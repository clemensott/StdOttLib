using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI.Core;

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
    }
}
