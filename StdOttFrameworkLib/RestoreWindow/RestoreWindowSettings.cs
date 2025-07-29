using System.Windows;

namespace StdOttFramework.RestoreWindow
{
    public struct RestoreWindowSettings
    {
        public bool RestoreLeft { get; set; }

        public bool RestoreTop { get; set; }

        public bool RestoreWidth { get; set; }

        public bool RestoreHeight { get; set; }

        public bool RestoreWindowState { get; set; }

        public WindowState? OverrideMinimized { get; set; }

        public StorePropertiesTriggerType StoreTriggerType { get; set; }

        public string FilePath { get; set; }

        public static RestoreWindowSettings GetDefault(bool restoreLeft = true, bool restoreTop = true,
            bool restoreWidth = true, bool restoreHeight = true, bool restoreWindowState = true,
            WindowState overrideMinimized = WindowState.Normal,
            StorePropertiesTriggerType triggerType = StorePropertiesTriggerType.Close,
            string filePath = null)
        {
            return new RestoreWindowSettings()
            {
                RestoreLeft = restoreLeft,
                RestoreTop = restoreTop,
                RestoreWidth = restoreWidth,
                RestoreHeight = restoreHeight,
                RestoreWindowState = restoreWindowState,
                OverrideMinimized = overrideMinimized,
                StoreTriggerType = triggerType,
                FilePath = filePath,
            };
        }
    }
}
