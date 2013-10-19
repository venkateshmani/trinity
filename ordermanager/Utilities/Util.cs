using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ordermanager.Utilities
{
    public class Util
    {
        public static Window GetParentWindow(UserControl control)
        {
            return Window.GetWindow(control);
        }
    }
}
