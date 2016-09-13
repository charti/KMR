using KMR.Common;
using KMR.View;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System;
using System.Windows;

namespace KMR.Control
{
    public class InputController : BaseController
    {
#region Member

        Grid _calcViewGrid;
        CalcEdit _edit;

        CalcInputs CurrentEdit;

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
                Messages.ValidationResult
            });

            CommandManager.RegisterClassCommandBinding(typeof(System.Windows.Controls.Control),
                new CommandBinding(Commands.OpenEditView, Edit_Click));
            CommandManager.RegisterClassCommandBinding(typeof(System.Windows.Controls.Control),
                new CommandBinding(Commands.FocusInput, Focus_Input));
            CommandManager.RegisterClassCommandBinding(typeof(System.Windows.Controls.Control),
                new CommandBinding(Commands.AcceptInput, Accept_Click));
            CommandManager.RegisterClassCommandBinding(typeof(System.Windows.Controls.Control),
                new CommandBinding(Commands.AbortInput, Abort_Click));
        }
        private CalcOption TransformStringToCalcOption(string option)
        {
            switch (option)
            {
                case "m²":
                case "Euro/m²":
                    return CalcOption.M2;
                case "Prozent":
                    return CalcOption.Percent;
                case "Euro/m² pro Monat":
                    return CalcOption.Month;
                case "Euro/m² pro Jahr":
                    return CalcOption.Year;
                case "Euro pro Wohneinheit":
                    return CalcOption.Unit;
                default:
                    throw new ArgumentOutOfRangeException(option);
            }
        }

        private void Accept_Click(object sender, ExecutedRoutedEventArgs e)
        {
            var val = new Tuple<CalcInputs, CalcOption, string>(
                CurrentEdit,
                TransformStringToCalcOption(_edit.comboUnit.SelectedValue.ToString()),
                _edit.txtInput.Text);

            Mediator.NotifyColleagues(Messages.Validate, val);
        }
        private void Abort_Click(object sender, ExecutedRoutedEventArgs e)
        {
            _calcViewGrid.Children.Remove(_edit);
            _edit = null;
        }

        private void Focus_Input(object sender, ExecutedRoutedEventArgs e)
        {
            (sender as CalcEdit)?.txtInput.Focus();
        }

        private void Edit_Click(object sender, ExecutedRoutedEventArgs e)
        {
            var row = ((Button)sender).GetValue(Grid.RowProperty);
            CurrentEdit = (CalcInputs)row;

            if (_edit != null)
            {
                if (_edit.GetValue(Grid.RowProperty).Equals(row))
                    return;
                _calcViewGrid.Children.Remove(_edit);
                _edit = null;
            }

            _edit = new CalcEdit((int)row, _comboItems[(int)row]);
            _calcViewGrid.Children.Add(_edit);
            _edit.Focusable = true;
            _edit.Focus();
        }

        public override void MessageNotification(string message, object args)
        {
            switch (message)
            {
                case Messages.ValidationResult:
                    var result = args as KMR.Model.ValidationResult;
                    ConsumeResult(result);
                    break;
                default:
                    new NotImplementedException();
                    break;
            }
        }

        private void ConsumeResult(KMR.Model.ValidationResult result)
        {
            if (result.Success)
            {
                _calcViewGrid.Children.Remove(_edit);
                _edit = null;
            }
            else
                MessageBox.Show(result.ErrorMsg, result.ErrorCaption, MessageBoxButton.OK);
        }
    }
}