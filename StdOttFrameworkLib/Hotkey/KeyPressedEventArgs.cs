using System;

namespace StdOttFramework.Hotkey
{
    public class KeyPressedEventArgs : EventArgs
    {
        public bool Handled { get; set; }

        public KeyPressedEventArgs(bool handled)
        {
            Handled = handled;
        }
    }
}