using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orodjarne.Models
{
    class PKStrojiModel : INotifyPropertyChanged
    {
        private int _id;
        private string _vrsta_stroja;
        //private string _vrsta_dela_id;

        public int Id
        {
            get { return _id; }
            set
            {
                if (value != _id)
                {
                    _id = value;
                    NotifyPropertyChanged("Id");
                }
            }
        }

        public string VrstaStroja
        {
            get { return _vrsta_stroja; }
            set
            {
                if (value != _vrsta_stroja)
                {
                    _vrsta_stroja = value;
                    NotifyPropertyChanged("VrstaStroja");
                }
            }
        }

        //public string VrstaDelaId
        //{
        //    get { return _vrsta_dela_id; }
        //    set
        //    {
        //        if (value != _vrsta_dela_id)
        //        {
        //            _vrsta_dela_id = value;
        //            NotifyPropertyChanged("VrstaDelaId");
        //        }
        //    }
        //}

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
