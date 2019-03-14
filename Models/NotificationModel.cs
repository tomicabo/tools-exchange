using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orodjarne.Models
{
    class NotificationModel : INotifyPropertyChanged
    {
        private string _novo_sporocilo;

        public string NovoSporocilo
        {
            get { return _novo_sporocilo; }
            set
            {
                if (value != _novo_sporocilo)
                {
                    _novo_sporocilo = value;
                    NotifyPropertyChanged("NovoSporocilo");
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
