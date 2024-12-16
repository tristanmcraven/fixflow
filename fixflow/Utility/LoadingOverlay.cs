using fixflow.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Media.Effects;

namespace fixflow.Utility
{
    class LoadingOverlay
    {
        public static void Show(FrameworkElement container)
        {
            //container.Effect = new BlurEffect { Radius = 15 };
            var bodyGrid = FindBodyGrid(container);

            if (bodyGrid == null)
                throw new InvalidOperationException();

            if (bodyGrid.Children.OfType<LoadingUserControl>().Any())
                return;

            var loadingOverlay = new LoadingUserControl
            {
                Name = "LoadingOverlay",
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
            };

            Grid.SetRowSpan(loadingOverlay, 10);
            Grid.SetColumnSpan(loadingOverlay, 10);

            Panel.SetZIndex(loadingOverlay, 2);

            bodyGrid.Children.Add(loadingOverlay);
        }

        public static void Remove(FrameworkElement container)
        {
            //container.Effect = null;
            var bodyGrid = FindBodyGrid(container);

            if (bodyGrid == null)
                throw new InvalidOperationException();

            var loadingOverlay = bodyGrid.Children.OfType<LoadingUserControl>().FirstOrDefault();
            if (loadingOverlay != null) bodyGrid.Children.Remove(loadingOverlay);
        }

        private static Grid FindBodyGrid(FrameworkElement container)
        {
            if (container is Window window)
            {
                return window.Content as Grid;
            }
            else if (container is Page page)
            {
                return page.Content as Grid;
            }
            return null;
        }
    }
}
