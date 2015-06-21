using System.Windows.Controls;

namespace KMR.View
{
    /// <summary>
    /// Interaktionslogik für Page2.xaml
    /// </summary>
    public partial class Calculation : UserControl
    {
        public Control.CalcController Controller { get; private set; }
        public Calculation()
        {
            Controller = new Control.CalcController();
            DataContext = Controller;
            InitializeComponent();
        }
    }
}
