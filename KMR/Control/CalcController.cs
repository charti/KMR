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
    public class CalcController : BaseController
    {
        public CalcController()
        {
            CommandManager.RegisterClassCommandBinding(typeof(System.Windows.Controls.Control),
                new CommandBinding(Commands.OpenMenuView, MenuBack_Click));
        }

        private void MenuBack_Click(object sender, ExecutedRoutedEventArgs e)
        {
            Mediator.NotifyColleagues(Messages.NavigateTo, typeof(View.Menu));
        }

        public override void MessageNotification(string message, object args)
        {
            switch (message)
            {

            }
        }
    }
}
