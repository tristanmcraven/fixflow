using fixflow.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace fixflow.Utility
{
    public class Settings
    {
        public bool? EnableAutoUpdates { get; set; }
        public bool? CheckForUpdates { get; set; }
        public bool? RememberWindowSize { get; set; }
        public bool? RememberWindowLocation { get; set; }
        public Dictionary<string, WindowSize> WindowSizes = new();
        public Dictionary<string, WindowLocation> WindowLocations = new();
        public AppTheme? AppTheme { get; set; }
        public AppLanguage? AppLanguage { get; set; }
    }

    public struct WindowSize
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public WindowState State { get; set; }

        public WindowSize(double width, double height, WindowState state)
        {
            Width = width;
            Height = height;
            State = state;
        }
    }

    public struct WindowLocation
    {
        public double Top { get; set; }
        public double Left { get; set; }
        public WindowState State { get; set; }
        public WindowLocation(double top, double left, WindowState state)
        {
            Top = top;
            Left = left;
            State = state;
        }
    }
}
