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
    class SporocilaViewModel
    {
        //string connectionString = "server = localhost; user = root; database = test_database; port = 3306; password = root123";
        //string connectionString = "server = sql11.freemysqlhosting.net; user = sql11201745; database = sql11201745; port = 3306; password = F7q23PrS1r";

        //int id_uporabnika = (Application.Current as App).uporabnikId;

        public ObservableCollection<SporocilaModel> sporocila = new ObservableCollection<SporocilaModel>();
        public ObservableCollection<SporocilaModel> Sporocila
        {
            get;
            set;
        }

        public SporocilaViewModel(int id_pogovora)
        {
            DobiSporocila(id_pogovora);
            Sporocila = sporocila;
        }

        public ObservableCollection<SporocilaModel> DobiSporocila(int id_pogovora)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["myDatabaseConnection"].ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                //var query = "select * from pogovori where posiljatelj = " + id_uporabnika + " or prejemnik = " + id_uporabnika + ";";
                //var query = "select * from sporocila where idpogovora = " + id_pogovora + ";";
                var query = "select sporocila.idsporocila as idsporocila, " +
                            "sporocila.ustvarjeno as ustvarjeno, " +
                            "sporocila.posiljatelj as posiljatelj, " +
                            "sporocila.sporocilo as sporocilo, " +
                            "sporocila.idpogovora as idpogovora, " +
                            "uporabniki.odgovorna_oseba as odgovorna_oseba " +
                            "from sporocila sporocila, uporabniki uporabniki " +
                            "where uporabniki.id = sporocila.posiljatelj and idpogovora = " + id_pogovora + " and sporocilo != '' order by ustvarjeno;";

                //var query = "select sporocila.idsporocila as idsporocila, " +
                //            "sporocila.ustvarjeno as ustvarjeno, " +
                //            "sporocila.posiljatelj as posiljatelj, " +
                //            "sporocila.sporocilo as sporocilo, " +
                //            "sporocila.idpogovora as idpogovora, " +
                //            "uporabniki.odgovorna_oseba as odgovorna_oseba, " +
                //            "sporocila.priponka as priponka, " +
                //            "priponke.priponka1 as ime_priponke " +
                //            "from sporocila sporocila join uporabniki uporabniki on uporabniki.id = sporocila.posiljatelj " +
                //            "left join priponke priponke on priponke.idpriponke = sporocila.priponka " +
                //            "where idpogovora = " + id_pogovora + " order by ustvarjeno; ";
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(query, connection))
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SporocilaModel sporocila_temp = new SporocilaModel();
                        sporocila_temp.IdSporocila = Convert.ToInt32(reader["idsporocila"]);
                        sporocila_temp.Ustvarjeno = Convert.ToDateTime(reader["ustvarjeno"]);
                        sporocila_temp.Posiljatelj = Convert.ToInt32(reader["posiljatelj"]);
                        sporocila_temp.Sporocilo = Convert.ToString(reader["sporocilo"]);
                        sporocila_temp.IdPogovora = Convert.ToInt32(reader["idpogovora"]);
                        sporocila_temp.OdgovornaOseba = Convert.ToString(reader["odgovorna_oseba"]);
                        //if (reader["priponka"] != DBNull.Value)
                        //{
                        //    sporocila_temp.Priponka = Convert.ToInt32(reader["priponka"]);
                        //    sporocila_temp.ImePriponke = Convert.ToString(reader["ime_priponke"]);
                        //}

                        sporocila.Add(sporocila_temp);

                    }
                }
                connection.Close();
                return sporocila;
            }
        }
    }
}
