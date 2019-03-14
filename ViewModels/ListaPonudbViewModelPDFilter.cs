using MySql.Data.MySqlClient;
using Orodjarne.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Drawing.Imaging;

namespace Orodjarne.ViewModels
{
    class ListaPonudbViewModelPDFilter
    {
        //string connectionString = "server = localhost; user = root; database = test_database; port = 3306; password = root123";
        //string connectionString = "server = sql11.freemysqlhosting.net; user = sql11201745; database = sql11201745; port = 3306; password = F7q23PrS1r";

        string izbrane_ponudbe;
        string query_tip;

        public ObservableCollection<ListaPonudbModel> lista = new ObservableCollection<ListaPonudbModel>();

        public ObservableCollection<ListaPonudbModel> ListaPonudb
        {
            get;
            set;
        }

        List<int> ponudbe = new List<int>();

        public ListaPonudbViewModelPDFilter(string id_ponudb)
        {
            DobiIdPonudb(id_ponudb);

            if (id_ponudb == "")
            {
                izbrane_ponudbe = "";
            }
            else if (id_ponudb != "")
            {
                if (ponudbe.Count == 0)
                    izbrane_ponudbe = "and id_ponudbe in (0)";
                else if (ponudbe.Count > 0)
                    izbrane_ponudbe = "and id_ponudbe in (" + string.Join(",", ponudbe.ToArray()) + ")";
            }

            //if (tip == 0)
            //    query_tip = "";
            //else if (tip == 1 || tip == 2)
            //    query_tip = "and tip_oglasa = " + tip + " ";
            
            DobiListoPonudb();
            ListaPonudb = lista;
        }

        public ObservableCollection<ListaPonudbModel> DobiListoPonudb()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["myDatabaseConnection"].ConnectionString;

            var query = "select id_ponudbe, group_concat(v.vrsta_dela separator ', ') as vrsta_dela, cena, datum_zacetka, datum_konca, ustvarjeno, opis, dimenzije, teza, material, tip_oglasa, id_uporabnika, " +
            "u.podjetje as podjetje, u.odgovorna_oseba as odgovorna_oseba, u.kraj as kraj, u.tel_st as tel_st, u.email as email, " +
            "u.logo as logo, priponke from lista_ponudb l " +
            "join uporabniki u on l.id_uporabnika = u.id " +
            "join delo_mapping m on l.vrsta_dela = m.id " +
            "join vrste_dela v on m.vrste_dela_id = v.id " +
            "where izbrisana_ponudba != 1 " + izbrane_ponudbe + " and l.id_uporabnika != " + (Application.Current as App).uporabnikId + " group by l.id_ponudbe order by id_ponudbe desc; ";


            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                
                MySqlCommand command = new MySqlCommand(query, connection);

                //try
                //{
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
                    if (reader["datum_zacetka"] != DBNull.Value)
                        ponudbe.DatumZacetka = Convert.ToDateTime(reader["datum_zacetka"]);
                    if (reader["datum_konca"] != DBNull.Value)
                        ponudbe.DatumKonca = Convert.ToDateTime(reader["datum_konca"]);
                    if (reader["ustvarjeno"] != DBNull.Value)
                        ponudbe.Ustvarjeno = Convert.ToDateTime(reader["ustvarjeno"]);
                    if (reader["opis"] != DBNull.Value)
                        ponudbe.Opis = Convert.ToString(reader["opis"]);
                    if (reader["dimenzije"] != DBNull.Value)
                        ponudbe.Dimenzije = Convert.ToString(reader["dimenzije"]);
                    if (reader["teza"] != DBNull.Value)
                            ponudbe.Teza = Convert.ToString(reader["teza"]);
                        if (reader["material"] != DBNull.Value)
                            ponudbe.Material = Convert.ToString(reader["material"]);
                        if (reader["tip_oglasa"] != DBNull.Value)
                            //ponudbe.TipOglasa = Convert.ToInt32(reader["tip_oglasa"]);
                            ponudbe.TipOglasa = "NUDIM DELO";
                        if (reader["id_uporabnika"] != DBNull.Value)
                            ponudbe.IdUporabnika = Convert.ToInt32(reader["id_uporabnika"]);
                        if (reader["podjetje"] != DBNull.Value)
                            ponudbe.UporabnikiPodjetje = Convert.ToString(reader["podjetje"]);
                        if (reader["odgovorna_oseba"] != DBNull.Value)
                            ponudbe.UporabnikiOdgovornaOseba = Convert.ToString(reader["odgovorna_oseba"]);
                        if (reader["kraj"] != DBNull.Value)
                            ponudbe.UporabnikiKraj = Convert.ToString(reader["kraj"]);
                        if (reader["tel_st"] != DBNull.Value)
                            ponudbe.UporabnikiTelSt = Convert.ToString(reader["tel_st"]);
                        if (reader["email"] != DBNull.Value)
                            ponudbe.UporabnikiEmail = Convert.ToString(reader["email"]);
                        if (reader["priponke"] != DBNull.Value)
                           ponudbe.Priponke = Convert.ToInt32(reader["priponke"]);



                        if (reader["logo"] != DBNull.Value)
                        {
                        byte[] blob = reader["logo"] as byte[];

                        using (MemoryStream ms = new MemoryStream())
                            {
                                ms.Write(blob, 0, blob.Length);
                                Bitmap bm = (Bitmap)Image.FromStream(ms);
                                using (MemoryStream msJpg = new MemoryStream())
                                {
                                    bm.Save(msJpg, ImageFormat.Jpeg);
                                    ponudbe.Logo = msJpg.GetBuffer();
                                }
                            }
                        }

                        lista.Add(ponudbe);
                    }

                    

                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.Message);
                //}
                connection.Close();
                return lista;

            }
        }

        public List<int> DobiIdPonudb(string idji)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["myDatabaseConnection"].ConnectionString;

                var query = "select l.id_ponudbe from delo_mapping m " +
                            "join lista_ponudb l on m.id = l.vrsta_dela " +
                            "where id in ('" + idji + "'); ";// group by m.id

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    MySqlCommand command = new MySqlCommand(query, connection);

                    try
                    {
                        connection.Open();
                        MySqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            ponudbe.Add(Convert.ToInt32(reader["id_ponudbe"]));
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    connection.Close();
                }
            //else if(tip == 2)
            //{
            //    var query = "select l.id_ponudbe from delo_mapping m " +
            //                "join lista_ponudb l on m.id = l.vrsta_dela " +
            //                "where vrste_dela_id in ('" + idji + "b'); ";// group by m.id

            //    using (MySqlConnection connection = new MySqlConnection(connectionString))
            //    {
            //        MySqlCommand command = new MySqlCommand(query, connection);

            //        try
            //        {
            //            connection.Open();
            //            MySqlDataReader reader = command.ExecuteReader();

            //            while (reader.Read())
            //            {
            //                ponudbe.Add(Convert.ToInt32(reader["id_ponudbe"]));
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show(ex.Message);
            //        }
            //        connection.Close();
            //    }
            //}
            return ponudbe;
        }
    }
}
