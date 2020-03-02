using System;
using Windows.ApplicationModel;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace StdOttUwp
{
    public class BackPressHandler
    {
        private static BackPressHandler instance;

        public static BackPressHandler Current
        {
            get
            {
                if (instance == null) instance = new BackPressHandler();

                return instance;
            }
        }

        private readonly SystemNavigationManager svm;
        private bool isSubscribed, isActivated;

        public event EventHandler<BackRequestedEventArgs> BackPressed;

        private BackPressHandler()
        {
            svm = SystemNavigationManager.GetForCurrentView();
            isActivated = false;
            isSubscribed = false;
        }

        public void Activate()
        {
            if (isActivated) return;
            isActivated = true;

            Application.Current.LeavingBackground += OnLeavingBackground;
            Application.Current.EnteredBackground += OnEnteredBackground;

            if (isSubscribed) return;
            isSubscribed = true;

            svm.BackRequested += OnBackRequested;
        }

        public void Deactivate()
        {
            if (!isActivated) return;
            isActivated = false;

            Application.Current.LeavingBackground -= OnLeavingBackground;
            Application.Current.EnteredBackground -= OnEnteredBackground;

            if (!isSubscribed) return;
            isSubscribed = false;

            svm.BackRequested -= OnBackRequested;
        }

        private void OnLeavingBackground(object sender, LeavingBackgroundEventArgs e)
        {
            if (isSubscribed) return;

            isSubscribed = true;
            svm.BackRequested += OnBackRequested;
        }

        private void OnEnteredBackground(object sender, EnteredBackgroundEventArgs e)
        {
            if(!isSubscribed)return;

            isSubscribed = false;
            svm.BackRequested -= OnBackRequested;
        }

        private void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            BackPressed?.Invoke(this, e);

            if (e.Handled) return;

            Frame frame = Window.Current.Content as Frame;

            if (frame?.CanGoBack == true)
            {
                e.Handled = true;
                frame.GoBack();
            }
        }
    }
}
