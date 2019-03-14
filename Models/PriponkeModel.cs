using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orodjarne.Models
{
    class PriponkeModel : INotifyPropertyChanged
    {
        private int _idpriponke;
        private string _priponka1;
        private string _priponka2;
        private string _priponka3;
        private string _priponka1_name;

        public int IdPriponke
        {
            get { return _idpriponke; }
            set
            {
                if (value != _idpriponke)
                {
                    _idpriponke = value;
                    NotifyPropertyChanged("IdPriponke");
                }
            }
        }

        public string Priponka1
        {
            get { return _priponka1; }
            set
            {
                if (value != _priponka1)
                {
                    _priponka1 = value;
                    NotifyPropertyChanged("Priponka1");
                }
            }
        }

        public string Priponka2
        {
            get { return _priponka2; }
            set
            {
                if (value != _priponka2)
                {
                    _priponka2 = value;
                    NotifyPropertyChanged("Priponka2");
                }
            }
        }

        public string Priponka3
        {
            get { return _priponka3; }
            set
            {
                if (value != _priponka3)
                {
                    _priponka3 = value;
                    NotifyPropertyChanged("Priponka3");
                }
            }
        }

        public string Priponka1Name
        {
            get { return _priponka1_name; }
            set
            {
                if (value != _priponka1_name)
                {
                    _priponka1_name = value;
                    NotifyPropertyChanged("Priponka1Name");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
