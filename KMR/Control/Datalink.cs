using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMR.Control
{
    public class DataLink : INotifyPropertyChanged
    {
        private string _member = "TESTSTRING";

        public double this[string key]
        {
            get { return 20; }
        }
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

    public class MyDict : Dictionary<string, double>
    {
        public MyDict()
        {

        }

        public double this[string key]
        {
            get { return this[key]; }
        }
    }
}
