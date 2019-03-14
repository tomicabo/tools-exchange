using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orodjarne.Models
{
    class VrsteDelaModel : INotifyPropertyChanged
    {
        private int _id;
        private string _vrsta_dela;
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

        public string VrstaDela
        {
            get { return _vrsta_dela; }
            set
            {
                if (value != _vrsta_dela)
                {
                    _vrsta_dela = value;
                    NotifyPropertyChanged("VrstaDela");
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
