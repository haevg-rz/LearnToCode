using GUI.View;

namespace GUI.Helper
{
    public static class WindowManager
    {
        private static ConfigWindow configWindow;

        public static void OpenConfigWindow()
        {
            configWindow = new ConfigWindow();
            configWindow.Show();
        }

        public static void CloseConfigWindow()
        {
            configWindow?.Close();
        }
    }
}
