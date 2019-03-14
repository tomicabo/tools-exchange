using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orodjarne.Models
{
    public class MojaListaPonudbModelPK : INotifyPropertyChanged
    {
        private int _id_ponudbe;
        private string _vrsta_dela;
        private string _vrsta_orodja;
        private DateTime _datum_zacetka;
        private DateTime _datum_konca;
        private DateTime _ustvarjeno;
        private string _opis;

        public int IdPonudbe
        {
            get { return _id_ponudbe; }
            set
            {
                if (value != _id_ponudbe)
                {
                    _id_ponudbe = value;
                    NotifyPropertyChanged("IdPonudbe");
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

        public DateTime DatumZacetka
        {
            get { return _datum_zacetka; }
            set
            {
                if (value != _datum_zacetka)
                {
                    _datum_zacetka = value;
                    NotifyPropertyChanged("DatumZacetka");
                }
            }
        }

        public DateTime DatumKonca
        {
            get { return _datum_konca; }
            set
            {
                if (value != _datum_konca)
                {
                    _datum_konca = value;
                    NotifyPropertyChanged("DatumKonca");
                }
            }
        }

        public DateTime Ustvarjeno
        {
            get { return _ustvarjeno; }
            set
            {
                if (value != _ustvarjeno)
                {
                    _ustvarjeno = value;
                    NotifyPropertyChanged("Ustvarjeno");
                }
            }
        }

        public string Opis
        {
            get { return _opis; }
            set
            {
                if (value != _opis)
                {
                    _opis = value;
                    NotifyPropertyChanged("Opis");
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
