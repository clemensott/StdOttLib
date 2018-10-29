using System;

namespace StdOttWpfLib.Hotkey
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