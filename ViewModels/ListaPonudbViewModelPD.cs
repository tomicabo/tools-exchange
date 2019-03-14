using MySql.Data.MySqlClient;
using Orodjarne.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Orodjarne.ViewModels
{
    class ListaPonudbViewModelPD
    {
        string connectionString = "server = localhost; user = root; database = test_database; port = 3306; password = root123";

        public ObservableCollection<ListaPonudbModel> lista = new ObservableCollection<ListaPonudbModel>();

        public ObservableCollection<ListaPonudbModel> ListaPonudb
        {
            get;
            set;
        }

        public ListaPonudbViewModelPD()
        {
            DobiListoPonudb();
            ListaPonudb = lista;
        }

        public ObservableCollection<ListaPonudbModel> DobiListoPonudb()
        {
            var query = "select id_ponudbe, group_concat(v.vrsta_dela separator ', ') as vrsta_dela, cena, datum_zacetka, datum_konca, opis, dimenzije, teza, material, id_uporabnika, " +
            "u.podjetje as podjetje, u.odgovorna_oseba as odgovorna_oseba, u.tel_st as tel_st, u.email as email, " +
            "u.logo as logo from lista_ponudb l " +
            "join uporabniki u on l.id_uporabnika = u.id " +
            "join delo_mapping m on l.vrsta_dela = m.id " +
            "join vrste_dela v on m.vrste_dela_id = v.vrsta_dela_id " +
            "where izbrisana_ponudba != 1 and l.id_uporabnika != " + (Application.Current as App).uporabnikId + " and tip_oglasa = 1 group by l.id_ponudbe order by id_ponudbe desc; ";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                
                MySqlCommand command = new MySqlCommand(query, connection);

                try
                {
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();


                    while (reader.Read())
                    {
                        ListaPonudbModel ponudbe = new ListaPonudbModel();

                        if (reader["id_ponudbe"] != DBNull.Value)
                            ponudbe.IdPonudbe = Convert.ToInt32(reader["id_ponudbe"]);
                        if (reader["vrsta_dela"] != DBNull.Value)
                            ponudbe.VrstaDela = Convert.ToString(reader["vrsta_dela"]);
                        if(reader["cena"] != DBNull.Value)
                            ponudbe.Cena = Convert.ToString(reader["cena"]);
                        if(reader["datum_zacetka"] != DBNull.Value)
                            ponudbe.DatumZacetka = Convert.ToDateTime(reader["datum_zacetka"]);
                        if (reader["datum_konca"] != DBNull.Value)
                            ponudbe.DatumKonca = Convert.ToDateTime(reader["datum_konca"]);
                        if (reader["opis"] != DBNull.Value)
                            ponudbe.Opis = Convert.ToString(reader["opis"]);
                        if (reader["dimenzije"] != DBNull.Value)
                            ponudbe.Dimenzije = Convert.ToString(reader["dimenzije"]);
                        if (reader["teza"] != DBNull.Value)
                            ponudbe.Teza = Convert.ToString(reader["teza"]);
                        if (reader["material"] != DBNull.Value)
                            ponudbe.Material = Convert.ToString(reader["material"]);
                        if (reader["id_uporabnika"] != DBNull.Value)
                            ponudbe.IdUporabnika = Convert.ToInt32(reader["id_uporabnika"]);
                        if (reader["podjetje"] != DBNull.Value)
                            ponudbe.UporabnikiPodjetje = Convert.ToString(reader["podjetje"]);
                        if (reader["odgovorna_oseba"] != DBNull.Value)
                            ponudbe.UporabnikiOdgovornaOseba = Convert.ToString(reader["odgovorna_oseba"]);
                        if (reader["tel_st"] != DBNull.Value)
                            ponudbe.UporabnikiTelSt = Convert.ToString(reader["tel_st"]);
                        if (reader["email"] != DBNull.Value)
                            ponudbe.UporabnikiEmail = Convert.ToString(reader["email"]);

                        if (!reader.IsDBNull(reader.GetOrdinal("logo")))
                        {
                            using (MemoryStream ms = new MemoryStream(reader.GetOrdinal("logo")))
                            {
                                //var imageSource = new BitmapImage();
                                //imageSource.BeginInit();
                                //imageSource.StreamSource = ms;
                                //imageSource.CacheOption = BitmapCacheOption.OnLoad;
                                //imageSource.EndInit();

                                //ponudbe.Logo = imageSource;
                                // Assign the Source property of your image
                                //yourImage.Source = imageSource;

                            }
                        }

                        lista.Add(ponudbe);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                connection.Close();
                    return lista;
                
            }
        }
    }
}
