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
    class PKStrojiViewModel
    {
        //string connectionString = "server = localhost; user = root; database = test_database; port = 3306; password = root123";
        //string connectionString = "server = sql11.freemysqlhosting.net; user = sql11201745; database = sql11201745; port = 3306; password = F7q23PrS1r";
        

        string query;

        public ObservableCollection<PKStrojiModel> vrste_stroja = new ObservableCollection<PKStrojiModel>();
        public ObservableCollection<PKStrojiModel> VrsteStroja
        {
            get;
            set;
        }

        public PKStrojiViewModel()
        {
            DobiVrsteStroja();
            VrsteStroja = vrste_stroja;
        }

        public ObservableCollection<PKStrojiModel> DobiVrsteStroja()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["myDatabaseConnection"].ConnectionString;

            query = "select id, vrsta_stroja from pk_stroji";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(query, connection))
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PKStrojiModel vrste_temp = new PKStrojiModel();
                        vrste_temp.Id = Convert.ToInt32(reader["id"]);
                        vrste_temp.VrstaStroja = Convert.ToString(reader["vrsta_stroja"]);
                        //vrste_temp.VrstaDelaId = Convert.ToString(reader["vrsta_dela_id"]);

                        vrste_stroja.Add(vrste_temp);

                    }
                }
                connection.Close();
                return vrste_stroja;
            }
        }
    }
}
