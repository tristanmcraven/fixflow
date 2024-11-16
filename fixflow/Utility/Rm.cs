using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fixflow.Utility
{
    class Rm
    {
        public static string Get(string key)
        {
            return App.Current.Resources[key] as string ?? "";
        }
    }
}
