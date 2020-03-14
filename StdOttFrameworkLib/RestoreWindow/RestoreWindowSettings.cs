namespace StdOttFramework.RestoreWindow
{
    public struct RestoreWindowSettings
    {
        public bool RestoreLeft { get; set; }

        public bool RestoreTop { get; set; }

        public bool RestoreWidth { get; set; }

        public bool RestoreHeight { get; set; }

        public StorePropertiesTriggerType StoreTriggerType { get; set; }

        public string FilePath { get; set; }

        public static RestoreWindowSettings GetDefault()
        {
            return new RestoreWindowSettings()
            {
                RestoreLeft = true,
                RestoreTop = true,
                RestoreWidth = true,
                RestoreHeight = true,
                StoreTriggerType = StorePropertiesTriggerType.Close,
                FilePath = null,
            };
        }
    }
}
