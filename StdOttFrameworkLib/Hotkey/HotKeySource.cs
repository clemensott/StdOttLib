namespace StdOttFramework.Hotkey
{
    public struct HotKeySource
    {
        public string SearchKey { get; private set; }

        public KeyPressedEventHandler Target { get; private set; }

        public HotKeySource(string searchKey, KeyPressedEventHandler target)
        {
            SearchKey = searchKey;
            Target = target;
        }
    }
}
