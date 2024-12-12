using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace fixflow.Utility
{
    class WindowManager
    {
        public static T Get<T>() where T : Window
        {
            return Application.Current.Windows.OfType<T>().First();
        }

        public static string GetName(Window window)
        {
            return window.GetType().Name;
        }

        public static void SetProperties(Window window)
        {
            var properties = SettingsManager.GetWindowSettings(GetName(window));

            if (App.Settings.RememberWindowSize == true)
            {
                var size = properties.size;
                if (size != null)
                {
                    window.Width = size.Value.Width;
                    window.Height = size.Value.Height;
                    window.WindowState = size.Value.State;
                }
            }
            if (App.Settings.RememberWindowLocation == true)
            {
                var location = properties.location;
                if (location != null)
                {
                    window.Top = location.Value.Top;
                    window.Left = location.Value.Left;
                    window.WindowState = location.Value.State;
                }
            }
        }
    }
}
