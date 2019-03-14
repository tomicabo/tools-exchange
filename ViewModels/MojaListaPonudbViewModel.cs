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
using System.Windows.Media.Imaging;

namespace Orodjarne.ViewModels
{
    class MojaListaPonudbViewModel
    {
        //string connectionString = "server = localhost; user = root; database = test_database; port = 3306; password = root123";
        //string connectionString = "server = sql11.freemysqlhosting.net; user = sql11201745; database = sql11201745; port = 3306; password = F7q23PrS1r";

        int id_uporabnika = (Application.Current as App).uporabnikId;

        public ObservableCollection<MojaListaPonudbModel> moja_lista = new ObservableCollection<MojaListaPonudbModel>();
        public ObservableCollection<MojaListaPonudbModel> MojaListaPonudb
        {
            get;
            set;
        }

        public MojaListaPonudbViewModel()
        {
            DobiListoPonudb();
            MojaListaPonudb = moja_lista;
        }

        public ObservableCollection<MojaListaPonudbModel> DobiListoPonudb()
        {

            var connectionString = ConfigurationManager.ConnectionStrings["myDatabaseConnection"].ConnectionString;


            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                var query = "select id_ponudbe, group_concat(v.vrsta_dela separator ', ') as vrsta_dela, ustvarjeno, cena, datum_zacetka, datum_konca, opis, dimenzije, teza, material, priponke " + 
                "from lista_ponudb l " + 
                "join delo_mapping m on l.vrsta_dela = m.id " + 
                "join vrste_dela v on m.vrste_dela_id = v.id " + 
                "where id_uporabnika = " + id_uporabnika + " and izbrisana_ponudba != 1 group by l.id_ponudbe order by id_ponudbe desc; ";

                connection.Open();
                using (MySqlCommand command = new MySqlCommand(query, connection))
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MojaListaPonudbModel ponudbe = new MojaListaPonudbModel();

                        if (reader["id_ponudbe"] != DBNull.Value)
                            ponudbe.IdPonudbe = Convert.ToInt32(reader["id_ponudbe"]);
                        if (reader["vrsta_dela"] != DBNull.Value)
                            ponudbe.VrstaDela = Convert.ToString(reader["vrsta_dela"]);
                        if (reader["cena"] != DBNull.Value)
                            ponudbe.Cena = Convert.ToString(reader["cena"]);
                        else ponudbe.Cena = "NI CENE";
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
                        if (reader["priponke"] != DBNull.Value)
                            ponudbe.Priponke = Convert.ToInt32(reader["priponke"]);


                        moja_lista.Add(ponudbe);

                        //if (!reader.IsDBNull(reader.GetOrdinal("logo")))
                        //{
                        //    byte[] blob = new byte[(reader.GetBytes(12, 0, null, 0, int.MaxValue))];
                        //    reader.GetBytes(12, 0, blob, 0, blob.Length);
                        //    using (MemoryStream ms = new MemoryStream())
                        //    {
                        //        ms.Write(blob, 0, blob.Length);
                        //        Bitmap bm = (Bitmap)Image.FromStream(ms);
                        //        using (MemoryStream msJpg = new MemoryStream())
                        //        {
                        //            bm.Save(msJpg, ImageFormat.Jpeg);
                        //            ponudbe.Logo = msJpg.GetBuffer();
                        //        }
                        //    }
                        //}
                        //lista_ponudb.Add(ponudbe);

                    }
                }
                connection.Close();
                return moja_lista;
            }
        }
    }
}
