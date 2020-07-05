using System;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace StdOttUwp
{
    public static class DialogUtils
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

        public static async Task<ContentDialogResult> ShowSafeAsync(this ContentDialog dialog)
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

        public static Task ShowSafeAsync(string content, string title = null)
        {
            return string.IsNullOrWhiteSpace(title) ?
                new MessageDialog(content).ShowSafeAsync() :
                new MessageDialog(content, title).ShowSafeAsync();
        }

        public static Task ShowSafeAsync(object obj, string title = null)
        {
            return ShowSafeAsync(obj?.ToString() ?? "<Null>", title);
        }

        public static async Task<bool> ShowTwoOptionsAsync(string message, string title, string defaultOptionText, string cancelOptionText)
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

        public static Task<ContentDialogResult> ShowContentAsync(object content, string title = null,
            string closeButtonText = "Close", string primaryButtonText = null, string secondaryButtonText = null,
            ContentDialogButton defaultButton = ContentDialogButton.Close)
        {
            ContentDialog dialog = new ContentDialog()
            {
                Content = content,
                DefaultButton = defaultButton,
            };

            if (!string.IsNullOrWhiteSpace(title)) dialog.Title = title;
            if (!string.IsNullOrWhiteSpace(primaryButtonText))
            {
                dialog.PrimaryButtonText = primaryButtonText;
                dialog.IsPrimaryButtonEnabled = true;
            }
            if (!string.IsNullOrWhiteSpace(secondaryButtonText))
            {
                dialog.SecondaryButtonText = secondaryButtonText;
                dialog.IsSecondaryButtonEnabled = true;
            }
            if (!string.IsNullOrWhiteSpace(closeButtonText)) dialog.CloseButtonText = closeButtonText;

            return dialog.ShowSafeAsync();
        }

        public static Task<ContentDialogResult> ShowYesNoCancelContentAsync(object content, string title = null,
            string closeButtonText = "Cancel", string primaryButtonText = "Yes", string secondaryButtonText = "No",
            ContentDialogButton defaultButton = ContentDialogButton.Primary)
        {
            return ShowContentAsync(content, title, closeButtonText, primaryButtonText, secondaryButtonText, defaultButton);
        }
    }
}
