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

namespace KMR
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class PageSwitcher : Window
    {
        private Control.Datalink _prop = new Control.Datalink();
        public PageSwitcher()
        {
            InitializeComponent();
            Style = FindResource(typeof(Window)) as Style;
            Switcher.pageSwitcher = this;
            Switcher.Switch(typeof(Menu));
        }
        public void Navigate(UserControl nextPage)
        {
            nextPage.DataContext = _prop;
            this.Content = nextPage;
        }
 
        public void Navigate(UserControl nextPage, object state)
        {
            this.Content = nextPage;
            ISwitchable s = nextPage as ISwitchable;
 
            if (s != null)
                s.UtilizeState(state);
            else
                throw new ArgumentException("NextPage is not ISwitchable! "
                  + nextPage.Name.ToString());
        }
    }
}
