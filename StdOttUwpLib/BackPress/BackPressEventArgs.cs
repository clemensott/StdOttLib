using System;

namespace StdOttUwp.BackPress
{
    public class BackPressEventArgs : EventArgs
    {
        public bool? CanGoBack { get; }

        public BackPressAction Action { get; set; }

        public BackPressEventArgs(bool? canGoBack, BackPressAction action)
        {
            CanGoBack = canGoBack;
            Action = action;
        }
    }
}
