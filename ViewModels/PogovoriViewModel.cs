using MySql.Data.MySqlClient;
using Orodjarne.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Orodjarne.ViewModels
{
    class PogovoriViewModel
    {
        //string connectionString = "server = localhost; user = root; database = test_database; port = 3306; password = root123";
        //string connectionString = "server = sql11.freemysqlhosting.net; user = sql11201745; database = sql11201745; port = 3306; password = F7q23PrS1r";

        int id_uporabnika = (Application.Current as App).uporabnikId;

        public ObservableCollection<PogovoriModel> pogovori = new ObservableCollection<PogovoriModel>();
        public ObservableCollection<PogovoriModel> Pogovori
        {
            get;
            set;
        }

        public PogovoriViewModel()
        {
            DobiPogovore();
            Pogovori = pogovori;
        }

        public ObservableCollection<PogovoriModel> DobiPogovore()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["myDatabaseConnection"].ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                //var query = "select * from pogovori where posiljatelj = " + id_uporabnika + " or prejemnik = " + id_uporabnika + ";";
                var query = "select pogovori.idpogovori as idpogovori, " +
                            "pogovori.ustvarjeno as ustvarjeno, " +
                            "pogovori.zadnje_sporocilo as zadnje_sporocilo, " +
                            "pogovori.zadeva as zadeva, " +
                            "pogovori.posiljatelj as posiljatelj, " +
                            "pogovori.prejemnik as prejemnik, " +
                            "pogovori.nov_pogovor_posiljatelj as nov_pogovor_posiljatelj, " +
                            "pogovori.nov_pogovor_prejemnik as nov_pogovor_prejemnik, " +
                            "uporabniki.id as iduporabnika, " +
                            "pogovori.posiljatelj_izbrisano as posiljatelj_izbrisano, " +
                            "pogovori.prejemnik_izbrisano as prejemnik_izbrisano, " +
                            "uporabniki.podjetje as podjetje, " +
                            "uporabniki.odgovorna_oseba as odgovorna_oseba " +
                            "from pogovori pogovori, uporabniki uporabniki " +
                            "where(uporabniki.id = pogovori.posiljatelj or uporabniki.id = pogovori.prejemnik) and(posiljatelj = " + id_uporabnika + " or prejemnik =  " + id_uporabnika + ") and uporabniki.id !=  " + id_uporabnika + "  order by zadnje_sporocilo desc;";
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(query, connection))
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //int je_posiljatelj = 0;
                        //int je_prejemnik = 0;
                        //int ne_prikazi_sporocila = 0;

                        PogovoriModel pogovori_temp = new PogovoriModel();
                        pogovori_temp.IdPogovori = Convert.ToInt32(reader["idpogovori"]);
                        pogovori_temp.Ustvarjeno = Convert.ToDateTime(reader["ustvarjeno"]);
                        pogovori_temp.ZadnjeSporocilo = Convert.ToDateTime(reader["zadnje_sporocilo"]);
                        pogovori_temp.Zadeva = Convert.ToString(reader["zadeva"]);
                        pogovori_temp.Posiljatelj = Convert.ToInt32(reader["posiljatelj"]);
                        pogovori_temp.Prejemnik = Convert.ToInt32(reader["prejemnik"]);
                        pogovori_temp.NovPogovorPrejemnik = Convert.ToInt32(reader["nov_pogovor_prejemnik"]);
                        pogovori_temp.NovPogovorPosiljatelj = Convert.ToInt32(reader["nov_pogovor_posiljatelj"]);
                        pogovori_temp.PosiljateljIzbrisano = Convert.ToInt32(reader["posiljatelj_izbrisano"]);
                        pogovori_temp.PrejemnikIzbrisano = Convert.ToInt32(reader["prejemnik_izbrisano"]);
                        pogovori_temp.IdUporabnika = Convert.ToInt32(reader["iduporabnika"]);
                        pogovori_temp.Podjetje = Convert.ToString(reader["podjetje"]);
                        pogovori_temp.OdgovornaOseba = Convert.ToString(reader["odgovorna_oseba"]);

                        if (pogovori_temp.Posiljatelj == id_uporabnika && pogovori_temp.NovPogovorPosiljatelj == 1)
                            pogovori_temp.OznaciPogovor = 1;
                        else if (pogovori_temp.Prejemnik == id_uporabnika && pogovori_temp.NovPogovorPrejemnik == 1)
                            pogovori_temp.OznaciPogovor = 1;
                        else pogovori_temp.OznaciPogovor = 0;

                        if(!(((pogovori_temp.Posiljatelj == id_uporabnika) && (pogovori_temp.PosiljateljIzbrisano == 1)) || ((pogovori_temp.Prejemnik == id_uporabnika) && (pogovori_temp.PrejemnikIzbrisano == 1))))
                        //if (pogovori_temp.Posiljatelj == pogovori_temp.IdUporabnika)
                        //    je_posiljatelj = 1;
                        //if (pogovori_temp.Prejemnik == pogovori_temp.IdUporabnika)
                        //    je_prejemnik = 1;
                        //if (je_posiljatelj == 1 && pogovori_temp.PosiljateljIzbrisano == 1)
                        //    ne_prikazi_sporocila = 1;
                        //if (je_prejemnik == 1 && pogovori_temp.PrejemnikIzbrisano == 1)
                        //    ne_prikazi_sporocila = 1;

                        //if (ne_prikazi_sporocila != 1)
                            pogovori.Add(pogovori_temp);

                    }
                }
                connection.Close();
                return pogovori;
            }
        }
    }
}
