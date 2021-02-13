using StdOttStandard;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace StdOttFramework.RestoreWindow
{
    public class RestoreWindowHandler
    {
        private static readonly IDictionary<Window, RestoreWindowHandler> handlers = new Dictionary<Window, RestoreWindowHandler>();

        public static RestoreWindowHandler Activate(Window window, RestoreWindowSettings settings)
        {
            RestoreWindowHandler handler;
            if (!handlers.TryGetValue(window, out handler))
            {
                handler = new RestoreWindowHandler(window, settings);
                handlers.Add(window, handler);
            }

            handler.Activate();

            return handler;
        }

        private bool isActivated;
        private readonly Window window;
        private readonly RestoreWindowSettings settings;

        public RestoreWindowHandler(Window window, RestoreWindowSettings settings)
        {
            this.window = window;
            this.settings = settings;
        }

        public void Activate()
        {
            if (isActivated) return;
            isActivated = false;

            try
            {
                Restore();
            }
            catch { }

            switch (settings.StoreTriggerType)
            {
                case StorePropertiesTriggerType.Close:
                    window.Closing += Window_Closing;
                    break;

                case StorePropertiesTriggerType.Invisible:
                    window.IsVisibleChanged += Window_IsVisibleChanged;
                    break;

                case StorePropertiesTriggerType.Deactivate:
                    window.Deactivated += Window_Deactivated;
                    break;

                case StorePropertiesTriggerType.PropertyChange:
                    window.SizeChanged += Window_SizeChanged;
                    window.LocationChanged += Window_LocationChanged;
                    window.StateChanged += Window_StateChanged;
                    break;
            }
        }

        public void Deactivate()
        {
            isActivated = false;

            window.Closing -= Window_Closing;
            window.IsVisibleChanged -= Window_IsVisibleChanged;
            window.Deactivated -= Window_Deactivated;
            window.SizeChanged -= Window_SizeChanged;
            window.LocationChanged -= Window_LocationChanged;
            window.StateChanged -= Window_StateChanged;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TryStore();
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            TryStore();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            TryStore();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            TryStore();
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            TryStore();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            TryStore();
        }

        public void Restore()
        {
            string filePath = GetFilePath(settings, window);
            RestoreWindowData data = StdUtils.XmlDeserializeFile<RestoreWindowData>(filePath);

            FitRestoreData(data);

            WindowState restoreState = settings.OverrideMinimized.HasValue && data.WindowState == WindowState.Minimized ?
                settings.OverrideMinimized.Value : data.WindowState;

            if (settings.RestoreWindowState && !window.IsLoaded)
            {
                window.WindowState = restoreState == WindowState.Normal ? WindowState.Normal : WindowState.Minimized;
            }

            if (settings.RestoreLeft) window.Left = data.Left;
            if (settings.RestoreTop) window.Top = data.Top;
            if (settings.RestoreWidth) window.Width = data.Width;
            if (settings.RestoreHeight) window.Height = data.Height;

            if (settings.RestoreWindowState)
            {
                if (!window.IsLoaded) window.Loaded += Window_Loaded;
                else window.WindowState = restoreState;
            }

            void Window_Loaded(object sender, RoutedEventArgs e)
            {
                window.Loaded -= Window_Loaded;

                window.WindowState = restoreState;
            }
        }

        private static string GetFilePath(RestoreWindowSettings settings, Window window)
        {
            if (!string.IsNullOrWhiteSpace(settings.FilePath)) return settings.FilePath;

            string windowName = window.GetType().FullName;

            return FrameworkUtils.GetFullPathToExe(windowName.Replace('.', '_') + ".xml");
        }

        private static void FitRestoreData(RestoreWindowData data)
        {
            if (data.Width > SystemParameters.VirtualScreenWidth) data.Width = SystemParameters.VirtualScreenWidth;
            if (data.Height > SystemParameters.VirtualScreenHeight) data.Height = SystemParameters.VirtualScreenHeight;

            if (data.Left < SystemParameters.VirtualScreenLeft) data.Left = SystemParameters.VirtualScreenLeft;
            else if (data.Left + data.Width > SystemParameters.VirtualScreenLeft + SystemParameters.VirtualScreenWidth)
            {
                data.Left = SystemParameters.VirtualScreenLeft + SystemParameters.VirtualScreenWidth - data.Width;
            }

            if (data.Top < SystemParameters.VirtualScreenTop) data.Top = SystemParameters.VirtualScreenTop;
            else if (data.Top + data.Height > SystemParameters.VirtualScreenTop + SystemParameters.VirtualScreenHeight)
            {
                data.Top = SystemParameters.VirtualScreenTop + SystemParameters.VirtualScreenHeight - data.Height;
            }
        }

        private void TryStore()
        {
            try
            {
                Store();
            }
            catch (Exception e)
            {
                try
                {
                    string filePath = $"Error_{GetFilePath(settings, window)}.txt";
                    File.WriteAllText(filePath, e.ToString());
                }
                catch { }
            }
        }

        public void Store()
        {
            string filePath = GetFilePath(settings, window);
            RestoreWindowData data = new RestoreWindowData()
            {
                Left = window.RestoreBounds.Left,
                Top = window.RestoreBounds.Top,
                Width = window.RestoreBounds.Width,
                Height = window.RestoreBounds.Height,
                WindowState = window.WindowState,
            };

            StdUtils.XmlSerialize(filePath, data);
        }
    }
}
