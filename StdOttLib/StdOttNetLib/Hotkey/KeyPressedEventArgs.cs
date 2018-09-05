using System;
using System.Windows.Input;

namespace StdOttWpfLib.Hotkey
{
    public class KeyPressedEventArgs : EventArgs
    {
        public bool Handled { get; set; }

        public Key Key { get; private set; }

        public KeyPressedEventArgs(Key key, bool handled)
        {
            Handled = handled;
            Key = key;
        }
    }
}