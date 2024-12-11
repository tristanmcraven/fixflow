using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fixflow.Model
{
    public class Settings
    {
        public bool Something { get; set; }
        public bool EnableAutoUpdates { get; set; }
        public bool CheckForUpdates { get; set; }
        public bool RememberWindowSize { get; set; }
        public bool RememberWindowLocation { get; set; }
        public Dictionary<string, WindowSize> WindowSizes { get; set; }
        public Dictionary<string, WindowLocation> WindowLocations { get; set; }
    }

    public struct WindowSize
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public WindowSize(double width, double height)
        {
            Width = width;
            Height = height;
        }
    }

    public struct WindowLocation
    {
        public double Top { get; set; }
        public double Left { get; set; }
        public WindowLocation(double top, double left)
        {
            Top = top;
            Left = left;
        }
    }
}
