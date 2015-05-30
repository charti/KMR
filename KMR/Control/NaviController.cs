using KMR.Common;
using KMR.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace KMR.Control
{
    public class NaviController : BaseController
    {
        private PageSwitcher MainWindow
        { get; set; }
        public View.Menu Menu
        { get; private set; }
        public Calculation Calculation
        { get; private set; }
        public NaviController(PageSwitcher mainWindow)
        {
            this.MainWindow = mainWindow;
            Menu = new View.Menu();
            Calculation = new Calculation();
            Mediator.Register(this, new[]
            {
                Messages.NavigateTo
            });
        }

        private void navigate(Object navTo)
        {
            switch (navTo.ToString())
            {
                case "KMR.View.Menu":
                    MainWindow.Content = Menu;
                    break;
                case "KMR.View.Calculation":
                    MainWindow.Content = Calculation;
                    break;
            }
        }

        public override void MessageNotification(string message, object args)
        {
            switch (message)
            {
                case Messages.NavigateTo:
                    navigate((Type)args);
                    break;
            }
        }
    }
}
