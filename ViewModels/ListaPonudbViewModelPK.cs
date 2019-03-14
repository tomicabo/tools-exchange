using MySql.Data.MySqlClient;
using Orodjarne.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Orodjarne.ViewModels
{
    class ListaPonudbViewModelPK
    {
        string izbrane_ponudbe;
        List<int> ponudbe = new List<int>();

        public ObservableCollection<ProsteKapaciteteModel> lista = new ObservableCollection<ProsteKapaciteteModel>();

        public ObservableCollection<ProsteKapaciteteModel> ListaPonudb
        {
            get;
            set;
        }

        public ListaPonudbViewModelPK(string id_ponudb)
        {
            DobiIdPonudb(id_ponudb);

            if (id_ponudb == "")
            {
                izbrane_ponudbe = "";
            }
            else if (id_ponudb != "")
            {
                if (ponudbe.Count == 0)
                    izbrane_ponudbe = "and id_proste_kapacitete in (0)";
                else if (ponudbe.Count > 0)
                    izbrane_ponudbe = "and id_proste_kapacitete in (" + string.Join(",", ponudbe.ToArray()) + ")";
            }
            DobiListoPonudb();
            ListaPonudb = lista;
        }

        public ObservableCollection<ProsteKapaciteteModel> DobiListoPonudb()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["myDatabaseConnection"].ConnectionString;

            var query = "select id_proste_kapacitete, group_concat(v.vrsta_stroja separator ', ') as vrsta_dela, datum_zacetka, datum_konca, ustvarjeno, opis, id_uporabnika, " +
                        "u.podjetje as podjetje, u.odgovorna_oseba as odgovorna_oseba, u.kraj as kraj, u.tel_st as tel_st, u.email as email, " +
                        "u.logo as logo from proste_kapacitete l " +
                        "join uporabniki u on l.id_uporabnika = u.id " +
                        "join pk_mapping m on l.vrsta_dela = m.id " +
                        "join pk_stroji v on m.vrste_strojev_id = v.id " +
                        "where izbrisana_ponudba != 1 "  + izbrane_ponudbe + " and l.id_uporabnika != " + (Application.Current as App).uporabnikId + " group by l.id_proste_kapacitete order by id_proste_kapacitete desc; ";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                
                MySqlCommand command = new MySqlCommand(query, connection);

                try
                {
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();


                    while (reader.Read())
                    {
                        ProsteKapaciteteModel ponudbe = new ProsteKapaciteteModel();

                        if (reader["id_proste_kapacitete"] != DBNull.Value)
                            ponudbe.IdPonudbe = Convert.ToInt32(reader["id_proste_kapacitete"]);
                        if (reader["vrsta_dela"] != DBNull.Value)
                            ponudbe.VrstaDela = Convert.ToString(reader["vrsta_dela"]);
                        if(reader["datum_zacetka"] != DBNull.Value)
                            ponudbe.DatumZacetka = Convert.ToDateTime(reader["datum_zacetka"]);
                        if (reader["datum_konca"] != DBNull.Value)
                            ponudbe.DatumKonca = Convert.ToDateTime(reader["datum_konca"]);
                        if (reader["ustvarjeno"] != DBNull.Value)
                            ponudbe.Ustvarjeno = Convert.ToDateTime(reader["ustvarjeno"]);
                        if (reader["opis"] != DBNull.Value)
                            ponudbe.Opis = Convert.ToString(reader["opis"]);
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

                        ponudbe.TipOglasa = "PROSTE KAPACITETE";
                        ponudbe.Priponke = 0;

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
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                connection.Close();
                    return lista;
                
            }
        }

        public List<int> DobiIdPonudb(string idji)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["myDatabaseConnection"].ConnectionString;

            var query = "select l.id_proste_kapacitete from pk_mapping m " +
                        "join proste_kapacitete l on m.id = l.vrsta_dela " +
                        "where vrste_strojev_id in ('" + idji + "'); ";// group by m.id

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);

                try
                {
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        ponudbe.Add(Convert.ToInt32(reader["id_proste_kapacitete"]));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                connection.Close();
            }
            return ponudbe;
        }
    }
}
