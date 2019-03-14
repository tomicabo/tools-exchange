using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orodjarne.Models
{
    class SporocilaModel : INotifyPropertyChanged
    {
        private int _idsporocila;
        private DateTime _ustvarjeno;
        private int _posiljatelj;
        private string _sporocilo;
        private int _idpogovora;
        private string _odgovorna_oseba;
        private int _priponka;
        private string _ime_priponke;

        public int IdSporocila
        {
            get { return _idsporocila; }
            set
            {
                if (value != _idsporocila)
                {
                    _idsporocila = value;
                    NotifyPropertyChanged("IdSporocila");
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

        public int Posiljatelj
        {
            get { return _posiljatelj; }
            set
            {
                if (value != _posiljatelj)
                {
                    _posiljatelj = value;
                    NotifyPropertyChanged("Posiljatelj");
                }
            }
        }

        public string Sporocilo
        {
            get { return _sporocilo; }
            set
            {
                if (value != _sporocilo)
                {
                    _sporocilo = value;
                    NotifyPropertyChanged("Sporocilo");
                }
            }
        }

        public int IdPogovora
        {
            get { return _idpogovora; }
            set
            {
                if (value != _idpogovora)
                {
                    _idpogovora = value;
                    NotifyPropertyChanged("IdPogovora");
                }
            }
        }

        public string OdgovornaOseba
        {
            get { return _odgovorna_oseba; }
            set
            {
                if (value != _odgovorna_oseba)
                {
                    _odgovorna_oseba = value;
                    NotifyPropertyChanged("OdgovornaOseba");
                }
            }
        }

        public int Priponka
        {
            get { return _priponka; }
            set
            {
                if (value != _priponka)
                {
                    _priponka = value;
                    NotifyPropertyChanged("Priponka");
                }
            }
        }

        public string ImePriponke
        {
            get { return _ime_priponke; }
            set
            {
                if (value != _ime_priponke)
                {
                    _ime_priponke = value;
                    NotifyPropertyChanged("ImePriponke");
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
