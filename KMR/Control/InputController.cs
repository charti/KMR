using KMR.Common;
using KMR.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace KMR.Control
{
    public class InputController : BaseController
    {
#region Member

        Grid _calcViewGrid;
        UserControl _edit;

        private Dictionary<int, string[]> _comboItems = new Dictionary<int, string[]>()
        {
            {1,     new [] {"Euro/m²"} },
            {2,     new [] {"Prozent", "Euro/m²"} },
            {6,     new [] {"Prozent"} },
            {7,     new [] {"Prozent"} },
            {10,    new [] {"Prozent"} },
            {11,    new [] {"Prozent"} },
            {15,    new [] {"Euro/m²"} },
            {16,    new [] {"Prozent", "Euro/m²"} },
            {20,    new [] {"Prozent"} },
            {21,    new [] {"Prozent"} },
            {24,    new [] {"Prozent"} },
            {25,    new [] {"Prozent"} },
            {29,    new [] {"Prozent", "Euro/m² pro Monat", "Euro/m² pro Jahr"} },
            {31,    new [] {"Prozent", "Euro/m² pro Monat", "Euro/m² pro Jahr"} },
            {33,    new [] {"Euro pro Wohneinheit", "Euro/m² pro Monat", "Euro/m² pro Jahr"} },
            {35,    new [] {"m²"} },
            {40,    new [] {"Prozent"} },
        };

#endregion

        public InputController(Grid calcView)
        {
            _calcViewGrid = calcView;
            Mediator.Register(this, new[]
            {
                Messages.Input
            });

            CommandManager.RegisterClassCommandBinding(typeof(System.Windows.Controls.Control),
                new CommandBinding(Commands.OpenEditView, Edit_Click));
        }

        private void Edit_Click(object sender, ExecutedRoutedEventArgs e)
        {
            var row = ((Button)sender).GetValue(Grid.RowProperty);

            if (_edit != null)
            {
                if (_edit.GetValue(Grid.RowProperty).Equals(row))
                    return;
                _calcViewGrid.Children.Remove(_edit);
                _edit = null;
            }

            _edit = new CalcEdit((int)row, _comboItems[(int)row]);
            _calcViewGrid.Children.Add(_edit);
            _edit.Focus();
        }

        public override void MessageNotification(string message, object args)
        {
            switch (message)
            {

            }
        }
    }
}