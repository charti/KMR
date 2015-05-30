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
    /// Interaktionslogik für CalcInput.xaml
    /// </summary>
    public partial class CalcInput : UserControl
    {
        public Control.InputController Controller { get; private set; }
        public CalcInput()
        {
            Controller = new Control.InputController();
            DataContext = Controller;
            InitializeComponent();
        }
    }
}
