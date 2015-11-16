using KMR.Common;
using KMR.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KMR.Control
{
    public class MenuController : BaseController
    {
        public MenuController()
        {
            CommandManager.RegisterClassCommandBinding(typeof(System.Windows.Controls.Control),
                new CommandBinding(Commands.OpenCalculationView, Calculation_Click));
        }

        #region Event Handler
        //event handler for the selection changed
        void Calculation_Click(object sender, RoutedEventArgs e)
        {
            Mediator.NotifyColleagues(Common.Messages.NavigateTo, typeof(Calculation));
        }
        #endregion

        public override void MessageNotification(string message, object args)
        {
        }
    }
}
