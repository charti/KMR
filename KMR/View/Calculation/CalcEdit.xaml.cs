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
    /// Interaktionslogik für CalcEdit.xaml
    /// </summary>
    public partial class CalcEdit : UserControl
    {
        public CalcEdit(int row, string[] comboItems)
        {
            this.SetValue(Grid.RowProperty, row);
            this.SetValue(Grid.ColumnProperty, 1);
            this.SetValue(Grid.RowSpanProperty, 20);
            InitializeComponent();

        }
    }
}
