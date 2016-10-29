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
            = new RoutedUICommand("Opens the Calculation View", "OpenCalculationView", typeof(Commands));

        public static RoutedUICommand OpenMenuView
            = new RoutedUICommand("Opens the Menu View", "OpenMenuView", typeof(Commands));

        public static RoutedUICommand OpenEditView
            = new RoutedUICommand("Opens the edit view", "OpenEditView", typeof(Commands));

        public static RoutedUICommand FocusInput
            = new RoutedUICommand("Focus the input field.", "FocusInput", typeof(Commands));

        public static RoutedUICommand AcceptInput
            = new RoutedUICommand("Accept input from Edit views.", "AcceptInput", typeof(Commands));

        public static RoutedUICommand AbortInput
            = new RoutedUICommand("Aborts input from Edit views.", "AbortInput", typeof(Commands));

        public static RoutedUICommand Save
            = new RoutedUICommand("Saves the current inputs to filesystem.", "Save", typeof(Commands));

        public static RoutedUICommand Load
            = new RoutedUICommand("Loads a KMR- save.", "Load", typeof(Commands));
    }
}
