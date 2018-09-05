using System;
using System.Windows.Input;

namespace StdOttWpfLib.Hotkey
{
    public class KeyPressedEventArgs : EventArgs
    {
        public Key Key { get; private set; }

        public KeyPressedEventArgs(Key key)
        {
            Key = key;
        }
    }
}