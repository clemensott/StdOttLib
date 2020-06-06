using System;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace StdOttUwp
{
    public static class MessageDialogUtils
    {
        private static readonly SemaphoreSlim showDialogSem = new SemaphoreSlim(1);

        public static async Task<IUICommand> ShowSafeAsync(this MessageDialog dialog)
        {
            await showDialogSem.WaitAsync();

            try
            {
                return await dialog.ShowAsync();
            }
            finally
            {
                showDialogSem.Release();
            }
        }

        public static Task ShowSafeAsync(string content)
        {
            return new MessageDialog(content).ShowSafeAsync();
        }

        public static Task ShowSafeAsync(string content, string title)
        {
            return new MessageDialog(content, title).ShowSafeAsync();
        }

        public static Task ShowSafeAsync(object obj, string title)
        {
            return ShowSafeAsync(obj.ToString(), title);
        }

        public static async Task<bool> Binary(string message, string title, string defaultOptionText, string cancelOptionText)
        {
            MessageDialog dialog = new MessageDialog(message, title);
            IUICommand cmdDefault = new UICommand(defaultOptionText);
            IUICommand cmdCancel = new UICommand(cancelOptionText);
            dialog.Commands.Add(cmdDefault);
            dialog.Commands.Add(cmdCancel);
            dialog.DefaultCommandIndex = 0;
            dialog.CancelCommandIndex = 1;

            IUICommand result = await dialog.ShowSafeAsync();

            return result == cmdDefault;
        }

        /// <summary>
        /// Return values: default = true , cancel = default and third option = null.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="defaultOptionText"></param>
        /// <param name="cancelOptionText"></param>
        /// <param name="thirdOptionText"></param>
        /// <returns></returns>
        public static async Task<bool?> Threestate(string message, string title, string defaultOptionText, string cancelOptionText, string thirdOptionText)
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

            IUICommand result = await dialog.ShowSafeAsync();

            if (result == cmdDefault) return true;
            if (result == cmdSecond) return false;
            return null;
        }
    }
}
