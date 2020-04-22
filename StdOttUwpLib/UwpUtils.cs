using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Popups;

namespace StdOttUwp
{
    public static class UwpUtils
    {
        public static async Task<bool> DialogBinary(string message, string title, string defaultOptionText, string cancelOptionText)
        {
            MessageDialog dialog = new MessageDialog(message, title);
            IUICommand cmdDefault = new UICommand(defaultOptionText);
            IUICommand cmdCancel = new UICommand(cancelOptionText);
            dialog.Commands.Add(cmdDefault);
            dialog.Commands.Add(cmdCancel);
            dialog.DefaultCommandIndex = 0;
            dialog.CancelCommandIndex = 1;

            IUICommand result = await dialog.ShowAsync();

            return result == cmdDefault;
        }

        public static async Task<bool?> DialogThreestate(string message, string title, string defaultOptionText, string cancelOptionText, string thirdOptionText)
        {
            MessageDialog dialog = new MessageDialog(message, title);
            IUICommand cmdDefault = new UICommand(defaultOptionText);
            IUICommand cmdSecond = new UICommand(cancelOptionText);
            IUICommand cmdThird = new UICommand(thirdOptionText);
            dialog.Commands.Add(cmdDefault);
            dialog.Commands.Add(cmdSecond);
            dialog.Commands.Add(cmdThird);
            dialog.DefaultCommandIndex = 0;
            dialog.CancelCommandIndex = 1;

            IUICommand result = await dialog.ShowAsync();

            if (result == cmdDefault) return true;
            if (result == cmdSecond) return false;
            return null;
        }

        public static async Task RunSafe(this DispatchedHandler handler, CoreDispatcherPriority priority = CoreDispatcherPriority.Normal)
        {
            CoreDispatcher dispatcher = CoreApplication.MainView.CoreWindow.Dispatcher;

            if (dispatcher.HasThreadAccess) handler();
            else await dispatcher.RunAsync(priority, handler);
        }
    }
}
