using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orodjarne.Models
{
    class UporabnikModel : INotifyPropertyChanged
    {
        private int _id;
        private string _podjetje;
        private string _odgovorna_oseba;
        private string _uporabnisko_ime;
        private string _geslo;
        private int _aktiven;
        private string _tel_st;
        private string _email;
        private byte[] _logo;
        private int _modul_lista_ponudb;
        private int _modul_moji_oglasi;
        private int _modul_prevozi;
        private int _modul_sporocila;

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

        public string UporabniskoIme
        {
            get { return _uporabnisko_ime; }
            set
            {
                if (value != _uporabnisko_ime)
                {
                    _uporabnisko_ime = value;
                    NotifyPropertyChanged("UporabniskoIme");
                }
            }
        }

        public string Geslo
        {
            get { return _geslo; }
            set
            {
                if (value != _geslo)
                {
                    _geslo = value;
                    NotifyPropertyChanged("Geslo");
                }
            }
        }

        public int Aktiven
        {
            get { return _aktiven; }
            set
            {
                if (value != _aktiven)
                {
                    _aktiven = value;
                    NotifyPropertyChanged("Aktiven");
                }
            }
        }

        public string TelSt
        {
            get { return _tel_st; }
            set
            {
                if (value != _tel_st)
                {
                    _tel_st = value;
                    NotifyPropertyChanged("TelSt");
                }
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                if (value != _email)
                {
                    _email = value;
                    NotifyPropertyChanged("Email");
                }
            }
        }

        public byte[] Logo
        {
            get { return _logo; }
            set
            {
                if (value != _logo)
                {
                    _logo = value;
                    NotifyPropertyChanged("Logo");
                }
            }
        }

        public int ModulListaPonudb
        {
            get { return _modul_lista_ponudb; }
            set
            {
                if (value != _modul_lista_ponudb)
                {
                    _modul_lista_ponudb = value;
                    NotifyPropertyChanged("ModulListaPonudb");
                }
            }
        }

        public int ModulMojiOglasi
        {
            get { return _modul_moji_oglasi; }
            set
            {
                if (value != _modul_moji_oglasi)
                {
                    _modul_moji_oglasi = value;
                    NotifyPropertyChanged("ModulMojiOglasi");
                }
            }
        }

        public int ModulPrevozi
        {
            get { return _modul_prevozi; }
            set
            {
                if (value != _modul_prevozi)
                {
                    _modul_prevozi = value;
                    NotifyPropertyChanged("ModulPrevozi");
                }
            }
        }

        public int ModulSporocila
        {
            get { return _modul_sporocila; }
            set
            {
                if (value != _modul_sporocila)
                {
                    _modul_sporocila = value;
                    NotifyPropertyChanged("ModulSporocila");
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
