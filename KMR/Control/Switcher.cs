using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace KMR
{
    public interface ISwitchable
    {
        void UtilizeState(object state);
    }
    public static class Switcher
    {
        public static PageSwitcher pageSwitcher;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewType">Has to be an UserControl (or inherited).</param>
        public static void Switch(Type viewType)
        {
            if (viewType.IsSubclassOf(typeof(UserControl)))
                pageSwitcher.Navigate(Activator.CreateInstance(viewType) as UserControl);
            else
                throw new ArgumentException("viewType is not inherrited by UserControl! "
                  + viewType.Name.ToString());
        }
    }
}