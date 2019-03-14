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
    class MojaListaPonudbViewModelPK
    {
        //string connectionString = "server = localhost; user = root; database = test_database; port = 3306; password = root123";
        //string connectionString = "server = sql11.freemysqlhosting.net; user = sql11201745; database = sql11201745; port = 3306; password = F7q23PrS1r";

        int id_uporabnika = (Application.Current as App).uporabnikId;

        public ObservableCollection<MojaListaPonudbModelPK> moja_lista = new ObservableCollection<MojaListaPonudbModelPK>();
        public ObservableCollection<MojaListaPonudbModelPK> MojaListaPonudb
        {
            get;
            set;
        }

        public MojaListaPonudbViewModelPK()
        {
            DobiListoPonudb();
            MojaListaPonudb = moja_lista;
        }

        public ObservableCollection<MojaListaPonudbModelPK> DobiListoPonudb()
        {

            var connectionString = ConfigurationManager.ConnectionStrings["myDatabaseConnection"].ConnectionString;


            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                var query = "select id_proste_kapacitete, group_concat(v.vrsta_stroja separator ', ') as vrsta_dela, ustvarjeno, datum_zacetka, datum_konca, opis " +
                "from proste_kapacitete l " +
                "join pk_mapping m on l.vrsta_dela = m.id " +
                "join pk_stroji v on m.vrste_strojev_id = v.id " +
                "where id_uporabnika = " + id_uporabnika + " and izbrisana_ponudba != 1 group by l.id_proste_kapacitete order by id_proste_kapacitete desc; ";

                connection.Open();
                using (MySqlCommand command = new MySqlCommand(query, connection))
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MojaListaPonudbModelPK ponudbe = new MojaListaPonudbModelPK();

                        if (reader["id_proste_kapacitete"] != DBNull.Value)
                            ponudbe.IdPonudbe = Convert.ToInt32(reader["id_proste_kapacitete"]);
                        if (reader["vrsta_dela"] != DBNull.Value)
                            ponudbe.VrstaDela = Convert.ToString(reader["vrsta_dela"]);
                        if (reader["datum_zacetka"] != DBNull.Value)
                            ponudbe.DatumZacetka = Convert.ToDateTime(reader["datum_zacetka"]);
                        if (reader["datum_konca"] != DBNull.Value)
                            ponudbe.DatumKonca = Convert.ToDateTime(reader["datum_konca"]);
                        if (reader["ustvarjeno"] != DBNull.Value)
                            ponudbe.Ustvarjeno = Convert.ToDateTime(reader["ustvarjeno"]);
                        if (reader["opis"] != DBNull.Value)
                            ponudbe.Opis = Convert.ToString(reader["opis"]);


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
