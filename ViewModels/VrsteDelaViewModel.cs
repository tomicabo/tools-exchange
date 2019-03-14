using MySql.Data.MySqlClient;
using Orodjarne.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Orodjarne.ViewModels
{
    class VrsteDelaViewModel
    {
        //string connectionString = "server = localhost; user = root; database = test_database; port = 3306; password = root123";
        //string connectionString = "server = sql11.freemysqlhosting.net; user = sql11201745; database = sql11201745; port = 3306; password = F7q23PrS1r";
        

        string query;

        public ObservableCollection<VrsteDelaModel> vrste_dela = new ObservableCollection<VrsteDelaModel>();
        public ObservableCollection<VrsteDelaModel> VrsteDela
        {
            get;
            set;
        }

        public VrsteDelaViewModel()
        {
            DobiVrsteDela();
            VrsteDela = vrste_dela;
        }

        public ObservableCollection<VrsteDelaModel> DobiVrsteDela()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["myDatabaseConnection"].ConnectionString;

            query = "select id, vrsta_dela from vrste_dela";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(query, connection))
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        VrsteDelaModel vrste_temp = new VrsteDelaModel
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            VrstaDela = Convert.ToString(reader["vrsta_dela"])
                        };
                        //vrste_temp.VrstaDelaId = Convert.ToString(reader["vrsta_dela_id"]);

                        vrste_dela.Add(vrste_temp);

                    }
                }
                connection.Close();
                return vrste_dela;
            }
        }
    }
}
