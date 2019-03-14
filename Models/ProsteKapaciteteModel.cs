using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Orodjarne.Models
{
    public class ProsteKapaciteteModel : INotifyPropertyChanged
    {
        private int _id_ponudbe;
        private string _vrsta_dela;
        private DateTime _datum_zacetka;
        private DateTime _datum_konca;
        private DateTime _ustvarjeno;
        private string _opis;
        private string _tip_oglasa;

        private int _id_uporabnika;
        private string _uporabniki_podjetje;
        private string _uporabniki_odgovorna_oseba;
        private string _uporabniki_kraj;
        private string _uporabniki_tel_st;
        private string _uporabniki_email;
        private byte[] _logo_image;

        private int _priponke;
        //private BitmapImage _logo_image;

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

        public string TipOglasa
        {
            get { return _tip_oglasa; }
            set
            {
                if (value != _tip_oglasa)
                {
                    _tip_oglasa = value;
                    NotifyPropertyChanged("TipOglasa");
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

        public int IdUporabnika
        {
            get { return _id_uporabnika; }
            set
            {
                if (value != _id_uporabnika)
                {
                    _id_uporabnika = value;
                    NotifyPropertyChanged("IdUporabnika");
                }
            }
        }

        public string UporabnikiPodjetje
        {
            get { return _uporabniki_podjetje; }
            set
            {
                if (value != _uporabniki_podjetje)
                {
                    _uporabniki_podjetje = value;
                    NotifyPropertyChanged("UporabnikiPodjetje");
                }
            }
        }

        public string UporabnikiOdgovornaOseba
        {
            get { return _uporabniki_odgovorna_oseba; }
            set
            {
                if (value != _uporabniki_odgovorna_oseba)
                {
                    _uporabniki_odgovorna_oseba = value;
                    NotifyPropertyChanged("UporabnikiOdgovornaOseba");
                }
            }
        }

        public string UporabnikiKraj
        {
            get { return _uporabniki_kraj; }
            set
            {
                if (value != _uporabniki_kraj)
                {
                    _uporabniki_kraj = value;
                    NotifyPropertyChanged("UporabnikiKraj");
                }
            }
        }

        public string UporabnikiTelSt
        {
            get { return _uporabniki_tel_st; }
            set
            {
                if (value != _uporabniki_tel_st)
                {
                    _uporabniki_tel_st = value;
                    NotifyPropertyChanged("UporabnikiTelSt");
                }
            }
        }

        public string UporabnikiEmail
        {
            get { return _uporabniki_email; }
            set
            {
                if (value != _uporabniki_email)
                {
                    _uporabniki_email = value;
                    NotifyPropertyChanged("UporabnikiEmail");
                }
            }
        }

        public byte[] Logo
        {
            get { return _logo_image; }
            set
            {
                if (value != _logo_image)
                {
                    _logo_image = value;
                    NotifyPropertyChanged("Logo");
                }
            }
        }

        //public BitmapImage Logo
        //{
        //    get { return _logo_image; }
        //    set
        //    {
        //        if (value != _logo_image)
        //        {
        //            _logo_image = value;
        //            NotifyPropertyChanged("Logo");
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
