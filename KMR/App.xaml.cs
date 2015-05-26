using KMR.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace KMR
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var app = (App)sender;
            app.MainWindow = new View.PageSwitcher();
            app.MainWindow.Show();
            var calculator = new Calculator();
        }
    }
}
