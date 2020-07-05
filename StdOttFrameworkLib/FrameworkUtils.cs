using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace StdOttFramework
{
    public static class FrameworkUtils
    {
        public static void InvokeSafe(Delegate method, params object[] args)
        {
            Dispatcher.CurrentDispatcher.Invoke(method, args);
        }

        public static TResult InvokeSafe<TResult>(Func<TResult> func)
        {
            return Dispatcher.CurrentDispatcher.Invoke(func);
        }

        public static string GetFullPathToExe(string relativePathToExe)
        {
            return Path.Combine(Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]), relativePathToExe);
        }

        public static CroppedBitmap CropImage(BitmapSource bmp, double ratio)
        {
            Int32Rect rect;

            if (bmp.PixelWidth < bmp.PixelHeight * ratio)
            {
                int height = (int)(bmp.PixelWidth / ratio);
                rect = new Int32Rect(0, (int)(bmp.PixelHeight - height) / 2, bmp.PixelWidth, height);
            }
            else
            {
                int width = (int)(bmp.PixelHeight * ratio);
                rect = new Int32Rect((int)(bmp.PixelWidth - width) / 2, 0, width, bmp.PixelHeight);
            }

            return new CroppedBitmap(bmp, rect);
        }

        public static T GetDataContext<T>(object sender)
        {
            return (T)((FrameworkElement)sender).DataContext;
        }

        public static bool TryGetDataContext<T>(object sender, out T dataContext)
        {
            if (sender is FrameworkElement element && element.DataContext is T)
            {
                dataContext = (T)element.DataContext;
                return true;
            }

            dataContext = default(T);
            return false;
        }

        public static T GetDataContextOrDefault<T>(object sender)
        {
            TryGetDataContext(sender, out T dataContext);
            return dataContext;
        }
    }
}