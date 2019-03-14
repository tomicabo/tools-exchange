using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orodjarne.Models
{
    class PogovoriModel : INotifyPropertyChanged
    {
        private int _idpogovori;
        private DateTime _ustvarjeno;
        private DateTime _zadnje_sporocilo;
        private string _zadeva;
        private int _posiljatelj;
        private int _prejemnik;
        private int _nov_pogovor_prejemnik;
        private int _nov_pogovor_posiljatelj;
        private int _oznaci_pogovor;
        private int _posiljatelj_izbrisano;
        private int _prejemnik_izbrisano;
        private int _iduporabnika;
        private string _podjetje;
        private string _odgovorna_oseba;

        public int IdPogovori
        {
            get { return _idpogovori; }
            set
            {
                if (value != _idpogovori)
                {
                    _idpogovori = value;
                    NotifyPropertyChanged("IdPogovori");
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

        public DateTime ZadnjeSporocilo
        {
            get { return _zadnje_sporocilo; }
            set
            {
                if (value != _zadnje_sporocilo)
                {
                    _zadnje_sporocilo = value;
                    NotifyPropertyChanged("ZadnjeSporocilo");
                }
            }
        }

        public string Zadeva
        {
            get { return _zadeva; }
            set
            {
                if (value != _zadeva)
                {
                    _zadeva = value;
                    NotifyPropertyChanged("Zadeva");
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

        public int Prejemnik
        {
            get { return _prejemnik; }
            set
            {
                if (value != _prejemnik)
                {
                    _prejemnik = value;
                    NotifyPropertyChanged("Prejemnik");
                }
            }
        }

        public int NovPogovorPrejemnik
        {
            get { return _nov_pogovor_prejemnik; }
            set
            {
                if (value != _nov_pogovor_prejemnik)
                {
                    _nov_pogovor_prejemnik = value;
                    NotifyPropertyChanged("NovPogovorPrejemnik");
                }
            }
        }

        public int NovPogovorPosiljatelj
        {
            get { return _nov_pogovor_posiljatelj; }
            set
            {
                if (value != _nov_pogovor_posiljatelj)
                {
                    _nov_pogovor_posiljatelj = value;
                    NotifyPropertyChanged("NovPogovor");
                }
            }
        }

        public int OznaciPogovor
        {
            get { return _oznaci_pogovor; }
            set
            {
                if (value != _oznaci_pogovor)
                {
                    _oznaci_pogovor = value;
                    NotifyPropertyChanged("OznaciPogovor");
                }
            }
        }

        public int PosiljateljIzbrisano
        {
            get { return _posiljatelj_izbrisano; }
            set
            {
                if (value != _posiljatelj_izbrisano)
                {
                    _posiljatelj_izbrisano = value;
                    NotifyPropertyChanged("PosiljateljIzbrisano");
                }
            }
        }

        public int PrejemnikIzbrisano
        {
            get { return _prejemnik_izbrisano; }
            set
            {
                if (value != _prejemnik_izbrisano)
                {
                    _prejemnik_izbrisano = value;
                    NotifyPropertyChanged("PrejemnikIzbrisano");
                }
            }
        }

        public string Podjetje
        {
            get { return _podjetje; }
            set
            {
                if (value != _podjetje)
                {
                    _podjetje = value;
                    NotifyPropertyChanged("Podjetje");
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

        public int IdUporabnika
        {
            get { return _iduporabnika; }
            set
            {
                if (value != _iduporabnika)
                {
                    _iduporabnika = value;
                    NotifyPropertyChanged("IdUporabnika");
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
