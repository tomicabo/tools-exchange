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

namespace Orodjarne.ViewModels
{
    class PrevoziPrevoznikiViewModel
    {
        int id_uporabnika = (Application.Current as App).uporabnikId;

        public ObservableCollection<PrevoziPrevoznikiModel> prevozi = new ObservableCollection<PrevoziPrevoznikiModel>();
        public ObservableCollection<PrevoziPrevoznikiModel> PrevoziPrevozniki
        {
            get;
            set;
        }

        public PrevoziPrevoznikiViewModel()
        {
            DobiPrevoze();
            PrevoziPrevozniki = prevozi;
        }

        public ObservableCollection<PrevoziPrevoznikiModel> DobiPrevoze()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["myDatabaseConnection"].ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                var query = "select idprevoza, ustvarjeno, datum_prevoza, cas_prevoza, start_lokacija, cilj_lokacija, id_uporabnika, opis, u.podjetje as podjetje, u.odgovorna_oseba as odgovorna_oseba, u.kraj as kraj, " +
                            "u.tel_st as tel_st, u.email as email, u.logo as logo from prevozi_prevozniki p " +
                            "join uporabniki u on p.id_uporabnika = u.id " +
                            "where izbrisano != 1 order by idprevoza desc; ";

                connection.Open();
                using (MySqlCommand command = new MySqlCommand(query, connection))
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PrevoziPrevoznikiModel prevozi_temp = new PrevoziPrevoznikiModel();
                        if(reader["idprevoza"] != DBNull.Value)
                            prevozi_temp.IdPrevoza = Convert.ToInt32(reader["idprevoza"]);
                        if (reader["ustvarjeno"] != DBNull.Value)
                            prevozi_temp.Ustvarjeno = Convert.ToDateTime(reader["ustvarjeno"]);
                        if (reader["datum_prevoza"] != DBNull.Value)
                            prevozi_temp.DatumPrevoza = Convert.ToDateTime(reader["datum_prevoza"]);
                        if (reader["cas_prevoza"] != DBNull.Value)
                            prevozi_temp.CasPrevoza = Convert.ToString(reader["cas_prevoza"]);
                        if (reader["start_lokacija"] != DBNull.Value)
                            prevozi_temp.StartLokacija = Convert.ToString(reader["start_lokacija"]);
                        if (reader["cilj_lokacija"] != DBNull.Value)
                            prevozi_temp.CiljLokacija = Convert.ToString(reader["cilj_lokacija"]);
                        if (reader["opis"] != DBNull.Value)
                            prevozi_temp.Opis = Convert.ToString(reader["opis"]);

                        if (reader["id_uporabnika"] != DBNull.Value)
                            prevozi_temp.IdUporabnika = Convert.ToInt32(reader["id_uporabnika"]);
                        if (reader["podjetje"] != DBNull.Value)
                            prevozi_temp.UporabnikiPodjetje = Convert.ToString(reader["podjetje"]);
                        if (reader["odgovorna_oseba"] != DBNull.Value)
                            prevozi_temp.UporabnikiOdgovornaOseba = Convert.ToString(reader["odgovorna_oseba"]);
                        if (reader["kraj"] != DBNull.Value)
                            prevozi_temp.UporabnikiKraj = Convert.ToString(reader["kraj"]);
                        if (reader["tel_st"] != DBNull.Value)
                            prevozi_temp.UporabnikiTelSt = Convert.ToString(reader["tel_st"]);
                        if (reader["email"] != DBNull.Value)
                            prevozi_temp.UporabnikiEmail = Convert.ToString(reader["email"]);

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
                                    prevozi_temp.Logo = msJpg.GetBuffer();
                                }
                            }
                        }

                        prevozi.Add(prevozi_temp);
                    }
                }
                connection.Close();
                return prevozi;
            }
        }
    }
}
