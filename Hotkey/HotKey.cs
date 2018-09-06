using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Input;
using System.Windows.Interop;

namespace StdOttWpfLib.Hotkey
{
    public delegate void KeyPressedEventHandler(object sender, KeyPressedEventArgs e);

    public class HotKey : IDisposable
    {
        public bool _disposed = false;

        public event KeyPressedEventHandler Pressed;

        public bool RegistrationSucessful { get; set; }

        public Key Key { get; private set; }

        public KeyModifier KeyModifiers { get; private set; }

        public int Id { get; set; }

        public HotKey(Key k, KeyModifier keyModifiers)
        {
            Key = k;
            KeyModifiers = keyModifiers;
        }

        private void Raise(ref bool handled)
        {
            KeyPressedEventArgs args = new KeyPressedEventArgs(Key, handled);
            Pressed?.Invoke(this, args);

            handled = args.Handled;
        }

        public bool Register()
        {
            return Service.Register(this);
        }

        public void Unregister()
        {
            Service.Unregister(this);
        }

        // Implement IDisposable.
        // Do not make this method virtual.
        // A derived class should not be able to override this method.
        public void Dispose()
        {
            Dispose(true);
            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        // Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the method has been called directly
        // or indirectly by a user's code. Managed and unmanaged resources
        // can be _disposed.
        // If disposing equals false, the method has been called by the
        // runtime from inside the finalizer and you should not reference
        // other objects. Only unmanaged resources can be _disposed.
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this._disposed)
            {
                // If disposing equals true, dispose all managed
                // and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                    Unregister();
                }

                // Note disposing has been done.
                _disposed = true;
            }
        }

        public static IEnumerable<HotKey> GetRegisteredHotKeys(string[] allKeyProperties, IEnumerable<HotKeySource> keySources)
        {
            foreach (HotKeySource source in keySources)
            {
                HotKey hk = GetHotKey(allKeyProperties, source.SearchKey);

                if (hk == null) continue;

                hk.Pressed += source.Target;
                hk.Register();

                yield return hk;
            }
        }

        private static HotKey GetHotKey(string[] hotKeyLines, string keyKey)
        {
            string hotKeyLine = hotKeyLines.FirstOrDefault(l => l.StartsWith(keyKey));

            if (hotKeyLine == null) return null;

            string[] parameter = hotKeyLine.Remove(0, keyKey.Length).Split(',');

            try
            {
                Key key;
                string keyString = parameter[0].Trim();
                int allModifier = 0;

                if (!Enum.TryParse(keyString, true, out key)) return null;

                for (int i = 1; i < parameter.Length; i++)
                {
                    KeyModifier modifier;
                    string modifierString = parameter[i].Trim().ToLower();

                    if (Enum.TryParse(modifierString, true, out modifier)) allModifier += (int)modifier;
                }

                return new HotKey(key, (KeyModifier)allModifier);
            }
            catch { }

            return null;
        }

        private static class Service
        {
            public const int WmHotKey = 0x0312;

            private static Dictionary<int, HotKey> dictHotKeyToCalBackProc;

            [DllImport("user32.dll")]
            private static extern bool RegisterHotKey(IntPtr hWnd, int id, UInt32 fsModifiers, UInt32 vlc);

            [DllImport("user32.dll")]
            private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

            public static bool Register(HotKey hk)
            {
                if (hk == null) return false;
                if (dictHotKeyToCalBackProc != null && dictHotKeyToCalBackProc.ContainsKey(hk.Id)) return false;

                int virtualKeyCode = KeyInterop.VirtualKeyFromKey(hk.Key);
                hk.Id = virtualKeyCode + ((int)hk.KeyModifiers * 0x10000);
                hk.RegistrationSucessful = RegisterHotKey(IntPtr.Zero, hk.Id, (uint)hk.KeyModifiers, (uint)virtualKeyCode);

                if (dictHotKeyToCalBackProc == null)
                {
                    dictHotKeyToCalBackProc = new Dictionary<int, HotKey>();
                    ComponentDispatcher.ThreadFilterMessage += new ThreadMessageEventHandler(ComponentDispatcherThreadFilterMessage);
                }

                dictHotKeyToCalBackProc.Add(hk.Id, hk);

                return hk.RegistrationSucessful;
            }

            public static void Unregister(HotKey hk)
            {
                if (hk == null || dictHotKeyToCalBackProc == null) return;
                if (!dictHotKeyToCalBackProc.ContainsKey(hk.Id)) return;

                UnregisterHotKey(IntPtr.Zero, hk.Id);
                dictHotKeyToCalBackProc.Remove(hk.Id);
            }

            private static void ComponentDispatcherThreadFilterMessage(ref MSG msg, ref bool handled)
            {
                if (handled || msg.message != WmHotKey) return;

                HotKey hotKey;
                if (!dictHotKeyToCalBackProc.TryGetValue((int)msg.wParam, out hotKey)) return;

                hotKey.Raise(ref handled);
            }
        }
    }
}
