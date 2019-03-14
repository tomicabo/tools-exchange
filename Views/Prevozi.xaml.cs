using MySql.Data.MySqlClient;
using Orodjarne.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Orodjarne.Views
{
    /// <summary>
    /// Interaction logic for Prevozi.xaml
    /// </summary>
    public partial class Prevozi : UserControl
    {
        public Prevozi()
        {
            InitializeComponent();
            rb_iskalci_prevozov.IsChecked = true;
        }



        private void rb_iskalci_prevozov_Checked(object sender, RoutedEventArgs e)
        {
            grid_iskalci_prevozov.Visibility = Visibility.Visible;
            grid_moji_prevozi.Visibility = Visibility.Hidden;
            lb_iskalci_prevozi.DataContext = new PrevoziIskalciViewModel();

        }

        private void rb_moji_prevozi_Checked(object sender, RoutedEventArgs e)
        {
            grid_moji_prevozi.Visibility = Visibility.Visible;
            grid_iskalci_prevozov.Visibility = Visibility.Hidden;
            lb_moji_prevozi.DataContext = new MojiPrevoziPrevoznikiViewModel();
        }


        private void btn_sporocilo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cb_cas_prevoza_DropDownOpened(object sender, EventArgs e)
        {
            var scrollViewerCombo = this.cb_cas_prevoza.Template.FindName("focus", this.cb_cas_prevoza) as ScrollViewer;
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

                    var queryGetId = "select max(idprevoza)+1 from prevozi_prevozniki;";
                    var query = "INSERT INTO prevozi_prevozniki (idprevoza, ustvarjeno, datum_prevoza, cas_prevoza, start_lokacija, cilj_lokacija, id_uporabnika, izbrisano, opis) VALUES(@IdPrevoza, @Ustvarjeno, @DatumPrevoza, @CasPrevoza, @StartLokacija, @CiljLokacija, @IdUporabnika, @Izbrisano, @Opis);";

                    connection.Open();

                    MySqlCommand commandgetid = new MySqlCommand(queryGetId, connection);
                    int id_prevoza = Convert.ToInt32(commandgetid.ExecuteScalar());

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IdPrevoza", id_prevoza);
                    command.Parameters.AddWithValue("@Ustvarjeno", DateTime.Now);
                    if(dp_datum_prevoza.SelectedDate != null)
                        command.Parameters.AddWithValue("@DatumPrevoza", dp_datum_prevoza.SelectedDate);
                    else command.Parameters.AddWithValue("@DatumPrevoza", null);
                    if(cb_cas_prevoza.SelectedItem != null)
                        command.Parameters.AddWithValue("@CasPrevoza", ((ComboBoxItem)cb_cas_prevoza.SelectedItem).Content.ToString());
                    else command.Parameters.AddWithValue("@CasPrevoza", null);
                    if(tb_od.Text != "")
                        command.Parameters.AddWithValue("@StartLokacija", tb_od.Text);
                    else command.Parameters.AddWithValue("@StartLokacija", null);
                    if(tb_do.Text != "")
                        command.Parameters.AddWithValue("@CiljLokacija", tb_do.Text);
                    else command.Parameters.AddWithValue("@CiljLokacija", null);
                    command.Parameters.AddWithValue("@IdUporabnika", (Application.Current as App).uporabnikId);
                    command.Parameters.AddWithValue("@Izbrisano", 0);
                    if(tb_opis.Text != "")
                        command.Parameters.AddWithValue("@Opis", tb_opis.Text);
                    else command.Parameters.AddWithValue("@Opis", null);

                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        private void btn_pocisti_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(((ComboBoxItem)cb_cas_prevoza.SelectedItem).Content.ToString());
        }
    }
}
