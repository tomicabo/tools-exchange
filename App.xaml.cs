using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Orodjarne
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public int uporabnikId = 0;
        public string podjetje;
        public string odgovorna_oseba;
        public string uporabnisko_ime;
        public string geslo;
        public string tel_st;
        public string email;
        public byte[] logo;
        public int novo_sporocilo;
        public int modul_lista_ponudb;
        public int modul_moji_oglasi;
        public int modul_prevozi;
        public int modul_sporocila;
    }
}
