using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KMR.Common
{
    /// <summary>
    /// Where all application commands are defined
    /// </summary>
    public static class Commands
    {
        public static RoutedUICommand OpenCalculationView
            = new RoutedUICommand("Open the Calculation View", "OpenCalculationView", typeof(Commands));

        public static RoutedUICommand OpenMenuView
            = new RoutedUICommand("Open the Menu View", "OpenMenuView", typeof(Commands));
        
    }
}
