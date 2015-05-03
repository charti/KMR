using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMR.Control
{
    public class Datalink : INotifyPropertyChanged
    {
        private string _member = "TESTSTRING";

        public string Eigenschaft
        {
            get { return _member; }
            set
            {
                if (value == _member)
                    return;
                _member = value;
                OnPropertyChanged("Eigenschaft");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
