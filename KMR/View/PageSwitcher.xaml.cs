using KMR.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KMR.View
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class PageSwitcher : Window
    {
        public NaviController Controller { get; private set; }
        public PageSwitcher()
        {
            Controller = new Control.NaviController(this);
            Style = FindResource(typeof(Window)) as Style;
            InitializeComponent();
            Controller.Mediator.NotifyColleagues("navigateTo", typeof(Menu));
        }
    }
}
