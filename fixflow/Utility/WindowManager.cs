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
    }
}
