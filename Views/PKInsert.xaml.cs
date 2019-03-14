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
using System.Windows.Shapes;

namespace Orodjarne.Views
{
    /// <summary>
    /// Interaction logic for PKInsert.xaml
    /// </summary>
    public partial class PKInsert : Window
    {
        public PKInsert()
        {
            InitializeComponent();
            lb_pk.DataContext = new PKStrojiViewModel();

        }

        private void btn_oddaj_oglas_pk_Click(object sender, RoutedEventArgs e)
        {
            if (lb_pk.SelectedItem != null)
            {
                var connectionString = ConfigurationManager.ConnectionStrings["myDatabaseConnection"].ConnectionString;

                MessageBoxResult result = MessageBox.Show("Ali želite oddati oglas?", "Oddaj Oglas", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        //Get proper int values from listbox vrsta_dela
                        List<int> selectedItemIndexes = new List<int>();
                        foreach (object i in lb_pk.SelectedItems)
                            selectedItemIndexes.Add(lb_pk.Items.IndexOf(i));
                        selectedItemIndexes.Sort();

                        connection.ConnectionString = connectionString;
                        var queryGetId = "select max(id_proste_kapacitete)+1 from proste_kapacitete;";
                        var getMaxIdVrstaDela = "select max(id)+1 from pk_mapping;";

                   //var query = "INSERT INTO proste_kapacitete (id_proste_kapacitete, vrsta_dela, datum_zacetka, datum_konca, ustvarjeno, opis, id_uporabnika, izbrisana_ponudba) values (@IdPonudbe, @VrstaDela, @DatumZacetka, @DatumKonca, @Ustvarjeno, @Opis, @IdUporabnika, @IzbrisanaPonudba);";
                        var queryInsertVrstaDela = "insert into pk_mapping (id, vrste_strojev_id) values (@IdVrsteStrojev, @SelectedItem);";

                        connection.Open();

                        MySqlCommand commandgetid = new MySqlCommand(queryGetId, connection);
                        int id_ponudbe = Convert.ToInt32(commandgetid.ExecuteScalar());

                        MySqlCommand commandgetmaxidvrstadela = new MySqlCommand(getMaxIdVrstaDela, connection);
                        int id_vrsta_stroja = Convert.ToInt32(commandgetmaxidvrstadela.ExecuteScalar());


                        using (MySqlCommand cmd = new MySqlCommand("insert_PK", connection))
                        {
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@IdPonudbe", id_ponudbe);
                            cmd.Parameters.AddWithValue("@VrstaDela", id_vrsta_stroja);
                            cmd.Parameters.AddWithValue("@DatumZacetka", dp_datum_zacetka_pk.SelectedDate);
                            cmd.Parameters.AddWithValue("@DatumKonca", dp_datum_konca_pk.SelectedDate);
                            cmd.Parameters.AddWithValue("@Opis", tb_opis_pk.Text);
                            cmd.Parameters.AddWithValue("@IdUporabnika", (Application.Current as App).uporabnikId);

                            //MySqlParameter param1 = new MySqlParameter("VrstaDela", MySqlDbType.Int32);
                            //MySqlParameter param2 = new MySqlParameter("DatumZacetka", MySqlDbType.DateTime);
                            //MySqlParameter param3 = new MySqlParameter("DatumKonca", MySqlDbType.DateTime);
                            ////MySqlParameter param4 = new MySqlParameter("Ustvarjeno", MySqlDbType.DateTime);
                            //MySqlParameter param5= new MySqlParameter("Opis", MySqlDbType.VarChar);
                            //MySqlParameter param6 = new MySqlParameter("IdUporabnika", MySqlDbType.Int32);
                            ////MySqlParameter param7 = new MySqlParameter("IzbrisanaPonudba", MySqlDbType.Int32);

                            //param1.Value = id_vrsta_stroja;
                            //param2.Value = dp_datum_zacetka_pk.SelectedDate;
                            //param3.Value = dp_datum_konca_pk.SelectedDate;
                            //param5.Value = tb_opis_pk.Text;
                            //param6.Value = (Application.Current as App).uporabnikId;

                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Vse ok");
                        }

                        //    MySqlCommand command = new MySqlCommand(query, connection);
                        //command.Parameters.AddWithValue("@IdPonudbe", id_ponudbe);
                        //command.Parameters.AddWithValue("@VrstaDela", id_vrsta_stroja);
                        //command.Parameters.AddWithValue("@DatumZacetka", dp_datum_zacetka_pk.SelectedDate);
                        //command.Parameters.AddWithValue("@DatumKonca", dp_datum_konca_pk.SelectedDate);
                        //command.Parameters.AddWithValue("@Ustvarjeno", DateTime.Now);
                        //command.Parameters.AddWithValue("@Opis", tb_opis_pk.Text);
                        //command.Parameters.AddWithValue("@IdUporabnika", (Application.Current as App).uporabnikId);
                        //command.Parameters.AddWithValue("@IzbrisanaPonudba", 0);

                        //command.ExecuteNonQuery();


                        for (int i = 0; i < selectedItemIndexes.Count; i++)
                        {
                            MySqlCommand commandVrstaDela = new MySqlCommand(queryInsertVrstaDela, connection);

                            commandVrstaDela.Parameters.AddWithValue("@IdVrsteStrojev", id_vrsta_stroja);
                            commandVrstaDela.Parameters.AddWithValue("@SelectedItem", selectedItemIndexes[i].ToString());
                            commandVrstaDela.ExecuteNonQuery();
                        }

                        connection.Close();
                    }
                }
                MessageBox.Show("Oglas je bil uspešno oddan");
                this.Close();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult = true;
        }
    }
}
