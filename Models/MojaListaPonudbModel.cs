using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orodjarne.Models
{
    public class MojaListaPonudbModel : INotifyPropertyChanged
    {
        private int _id_ponudbe;
        private string _vrsta_dela;
        private string _vrsta_orodja;
        private string _cena;
        private DateTime _datum_zacetka;
        private DateTime _datum_konca;
        private DateTime _ustvarjeno;
        private string _opis;
        private string _dimenzije;
        private string _teza;
        private string _material;
        private int _priponke;

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

        public string VrstaOrodja
        {
            get { return _vrsta_orodja; }
            set
            {
                if (value != _vrsta_orodja)
                {
                    _vrsta_orodja = value;
                    NotifyPropertyChanged("VrstaOrodja");
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

        public string Cena
        {
            get { return _cena; }
            set
            {
                if (value != _cena)
                {
                    _cena = value;
                    NotifyPropertyChanged("Cena");
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

        public string Dimenzije
        {
            get { return _dimenzije; }
            set
            {
                if (value != _dimenzije)
                {
                    _dimenzije = value;
                    NotifyPropertyChanged("Dimenzije");
                }
            }
        }

        public string Teza
        {
            get { return _teza; }
            set
            {
                if (value != _teza)
                {
                    _teza = value;
                    NotifyPropertyChanged("Teza");
                }
            }
        }

        public string Material
        {
            get { return _material; }
            set
            {
                if (value != _material)
                {
                    _material = value;
                    NotifyPropertyChanged("Material");
                }
            }
        }

        public int Priponke
        {
            get { return _priponke; }
            set
            {
                if (value != _priponke)
                {
                    _priponke = value;
                    NotifyPropertyChanged("Priponke");
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
