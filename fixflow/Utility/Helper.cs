using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fixflow.Utility
{
    public class Helper
    {
        public static bool IsNumeric(string c)
        {
            return Int32.TryParse(c, out _);
        }
    }
}
