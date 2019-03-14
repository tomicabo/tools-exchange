using MySql.Data.MySqlClient;
using Orodjarne.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows.Input;

namespace Orodjarne.ViewModels
{
    class PriponkeViewModel
    {
        public ObservableCollection<PriponkeModel> priponke = new ObservableCollection<PriponkeModel>();
        public ObservableCollection<PriponkeModel> Priponke
        {
            get;
            set;
        }

        public PriponkeViewModel(int idpriponk)
        {
            DobiPriponke(idpriponk);
            Priponke = priponke;
        }

        public ObservableCollection<PriponkeModel> DobiPriponke(int idpriponk)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["myDatabaseConnection"].ConnectionString;
            string query = "select * from priponke where idpriponke = " + idpriponk + ";";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(query, connection))
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PriponkeModel priponke_temp = new PriponkeModel();
                        priponke_temp.IdPriponke = Convert.ToInt32(reader["idpriponke"]);
                        priponke_temp.Priponka1 = Convert.ToString(reader["priponka1"]);
                        priponke_temp.Priponka2 = Convert.ToString(reader["priponka2"]);
                        priponke_temp.Priponka3 = Convert.ToString(reader["priponka3"]);
                        priponke_temp.Priponka1Name = Convert.ToString(reader["priponka1_name"]);

                        priponke.Add(priponke_temp);

                    }
                }
                connection.Close();
                return priponke;
            }
        }
    }
}
