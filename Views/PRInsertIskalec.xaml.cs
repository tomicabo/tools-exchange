using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Orodjarne.Views
{
    /// <summary>
    /// Interaction logic for PRInsertIskalec.xaml
    /// </summary>
    public partial class PRInsertIskalec : Window
    {
        public PRInsertIskalec()
        {
            InitializeComponent();
        }

        private void btn_dodaj_prevoz_Click(object sender, RoutedEventArgs e)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["myDatabaseConnection"].ConnectionString;

            MessageBoxResult result = MessageBox.Show("Ali želite oddati oglas?", "Oddaj Oglas", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.ConnectionString = connectionString;

                    var queryGetId = "select max(idprevoza)+1 from prevozi_iskanje;";
                    var query = "INSERT INTO prevozi_iskanje (idprevoza, ustvarjeno, datum_prevoza, cas_prevoza, start_lokacija, cilj_lokacija, id_uporabnika, izbrisano, opis) VALUES(@IdPrevoza, @Ustvarjeno, @DatumPrevoza, @CasPrevoza, @StartLokacija, @CiljLokacija, @IdUporabnika, @Izbrisano, @Opis);";

                    connection.Open();

                    MySqlCommand commandgetid = new MySqlCommand(queryGetId, connection);
                    int id_prevoza = Convert.ToInt32(commandgetid.ExecuteScalar());

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IdPrevoza", id_prevoza);
                    command.Parameters.AddWithValue("@Ustvarjeno", DateTime.Now);
                    if (dp_datum_prevoza.SelectedDate != null)
                        command.Parameters.AddWithValue("@DatumPrevoza", dp_datum_prevoza.SelectedDate);
                    else command.Parameters.AddWithValue("@DatumPrevoza", null);
                    if (cb_cas_prevoza.SelectedItem != null)
                        command.Parameters.AddWithValue("@CasPrevoza", ((ComboBoxItem)cb_cas_prevoza.SelectedItem).Content.ToString());
                    else command.Parameters.AddWithValue("@CasPrevoza", null);
                    if (tb_od.Text != "")
                        command.Parameters.AddWithValue("@StartLokacija", tb_od.Text);
                    else command.Parameters.AddWithValue("@StartLokacija", null);
                    if (tb_do.Text != "")
                        command.Parameters.AddWithValue("@CiljLokacija", tb_do.Text);
                    else command.Parameters.AddWithValue("@CiljLokacija", null);
                    command.Parameters.AddWithValue("@IdUporabnika", (Application.Current as App).uporabnikId);
                    command.Parameters.AddWithValue("@Izbrisano", 0);
                    if (tb_opis.Text != "")
                        command.Parameters.AddWithValue("@Opis", tb_opis.Text);
                    else command.Parameters.AddWithValue("@Opis", null);

                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
    }
}
