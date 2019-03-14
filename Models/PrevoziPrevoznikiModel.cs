using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orodjarne.Models
{
    class PrevoziPrevoznikiModel : INotifyPropertyChanged
    {
        private int _id_prevoza;
        private DateTime _ustvarjeno;
        private DateTime _datum_prevoza;
        private string _cas_prevoza;
        private string _start_lokacija;
        private string _cilj_lokacija;
        private string _opis;

        private int _id_uporabnika;
        private string _uporabniki_podjetje;
        private string _uporabniki_odgovorna_oseba;
        private string _uporabniki_kraj;
        private string _uporabniki_tel_st;
        private string _uporabniki_email;
        private byte[] _logo_image;

        public int IdPrevoza
        {
            get { return _id_prevoza; }
            set
            {
                if (value != _id_prevoza)
                {
                    _id_prevoza = value;
                    NotifyPropertyChanged("IdPrevoza");
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

        public DateTime DatumPrevoza
        {
            get { return _datum_prevoza; }
            set
            {
                if (value != _datum_prevoza)
                {
                    _datum_prevoza = value;
                    NotifyPropertyChanged("DatumPrevoza");
                }
            }
        }

        public string CasPrevoza
        {
            get { return _cas_prevoza; }
            set
            {
                if (value != _cas_prevoza)
                {
                    _cas_prevoza = value;
                    NotifyPropertyChanged("CasPrevoza");
                }
            }
        }

        public string StartLokacija
        {
            get { return _start_lokacija; }
            set
            {
                if (value != _start_lokacija)
                {
                    _start_lokacija = value;
                    NotifyPropertyChanged("StartLokacija");
                }
            }
        }

        public string CiljLokacija
        {
            get { return _cilj_lokacija; }
            set
            {
                if (value != _cilj_lokacija)
                {
                    _cilj_lokacija = value;
                    NotifyPropertyChanged("CiljLokacija");
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
