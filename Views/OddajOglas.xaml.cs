using Microsoft.Win32;
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
using System.ComponentModel;
using System.IO;
using System.Net;

namespace Orodjarne.Views
{
    /// <summary>
    /// Interaction logic for OddajOglas.xaml
    /// </summary>
    public partial class OddajOglas : UserControl
    {
        //string listbox_selection = "";
        //string connectionString = "server = localhost; user = root; database = test_database; port = 3306; password = root123";
        //string connectionString = "server = sql11.freemysqlhosting.net; user = sql11201745; database = sql11201745; port = 3306; password = F7q23PrS1r";


        BackgroundWorker worker = new BackgroundWorker();
        //private string fileName1 = "";
        //private string fileName2 = "";
        //private string fileName3 = "";
        //private string fullName1 = "";
        //private string fullName2 = "";
        //private string fullName3 = "";
        //private string fileName1link = "";
        private string userName = "test";
        private string password = "test";
        private string server = "ftp://192.168.1.125";
        int active_priponka = 1;

        List<string> fileName = new List<string>();
        List<string> fullName = new List<string>();
        List<string> fileNameLink = new List<string>();


        public OddajOglas()
        {
            InitializeComponent();

            radioButton.IsChecked = true;
            //listBox.DataContext = new VrsteDelaViewModel(1);
            

            //worker.WorkerReportsProgress = true;

            //worker.DoWork += worker_DoWork;
            //worker.ProgressChanged += worker_ProgressChanged;
            //worker.RunWorkerCompleted += worker_RunWorkerCompleted;
        }

        private void radioButton_Checked(object sender, RoutedEventArgs e)
        {
            //grid_delo.Visibility = Visibility.Visible;
            //grid_proste_kapacitete.Visibility = Visibility.Hidden;
            sp_delo.Visibility = Visibility.Visible;
            sp_proste_kapacitete.Visibility = Visibility.Hidden;
            lb_pd.Visibility = Visibility.Visible;
            lb_pd.DataContext = new VrsteDelaViewModel();
            lb_pk.Visibility = Visibility.Collapsed;
            listbox_header.Text = "VRSTA DELA";
            
        }

        private void radioButton2_Checked(object sender, RoutedEventArgs e)
        {
            //grid_delo.Visibility = Visibility.Hidden;
            //grid_proste_kapacitete.Visibility = Visibility.Visible;
            sp_delo.Visibility = Visibility.Hidden;
            sp_proste_kapacitete.Visibility = Visibility.Visible;
            lb_pk.Visibility = Visibility.Visible;
            lb_pk.DataContext = new PKStrojiViewModel();
            lb_pd.Visibility = Visibility.Collapsed;
            listbox_header.Text = "VRSTA STROJA";
        }

        private void radioButton2_Unchecked(object sender, RoutedEventArgs e)
        {
            //grid_delo.Visibility = Visibility.Visible;
            //grid_proste_kapacitete.Visibility = Visibility.Hidden;
            //listBox.DataContext = new VrsteDelaViewModel(1);

        }

        private void radioButton_Unchecked(object sender, RoutedEventArgs e)
        {
            //grid_delo.Visibility = Visibility.Hidden;
            //grid_proste_kapacitete.Visibility = Visibility.Visible;
            //listBox2.DataContext = new VrsteDelaViewModel(2);
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
                //int parse;
                //int flag = 0;

                //if (//tb_vrsta_dela.Text.Trim().Length > 0 &&
                //    listBox.SelectedItem != null &&
                //    tb_dimenzije.Text.Trim().Length > 0 &&
                //    tb_teza.Text.Trim().Length > 0 &&
                //    tb_material.Text.Trim().Length > 0 &&
                //    //tb_vrsta_orodja.Text.Trim().Length > 0 &&

                //    ((rb_cena.IsChecked == true && (int.TryParse(tb_cena.Text, out parse) || tb_cena.Text.Trim().Length > 0)) || (rb_cena_podogovoru.IsChecked == true) &&

                //    dp_datum_konca.Text.Trim().Length > 0 &&
                //    dp_datum_zacetka.Text.Trim().Length > 0))

                //    flag = 1;
                //else
                //{
                //    //if (tb_vrsta_dela.Text.Trim().Length == 0)
                //    //{
                //    //    tb_vrsta_dela.BorderBrush = new SolidColorBrush(Colors.Red);
                //    //    flag = 0;
                //    //}
                //    //else if (tb_vrsta_dela.Text.Trim().Length > 0)
                //    //    tb_vrsta_dela.BorderBrush = new SolidColorBrush(Colors.Black);

                //    if (listBox.SelectedItem == null)
                //    {
                //        listBox.BorderBrush = new SolidColorBrush(Colors.Red);
                //        flag = 0;
                //    }
                //    else if (listBox.SelectedItem != null)
                //        listBox.BorderBrush = new SolidColorBrush(Colors.Black);


                //    if (tb_dimenzije.Text.Trim().Length == 0)
                //    {
                //        tb_dimenzije.BorderBrush = new SolidColorBrush(Colors.Red);
                //        flag = 0;
                //    }
                //    else if (tb_dimenzije.Text.Trim().Length > 0)
                //        tb_dimenzije.BorderBrush = new SolidColorBrush(Colors.Black);

                //    if (tb_teza.Text.Trim().Length == 0)
                //    {
                //        tb_teza.BorderBrush = new SolidColorBrush(Colors.Red);
                //        flag = 0;
                //    }
                //    else if (tb_teza.Text.Trim().Length > 0)
                //        tb_teza.BorderBrush = new SolidColorBrush(Colors.Black);

                //    if (tb_material.Text.Trim().Length == 0)
                //    {
                //        tb_material.BorderBrush = new SolidColorBrush(Colors.Red);
                //        flag = 0;
                //    }
                //    else if (tb_material.Text.Trim().Length > 0)
                //        tb_material.BorderBrush = new SolidColorBrush(Colors.Black);

                //    //if (!int.TryParse(tb_cena.Text, out parse) || tb_cena.Text.Trim().Length == 0)
                //    //{
                //    //    tb_cena.BorderBrush = new SolidColorBrush(Colors.Red);
                //    //    flag = 0;
                //    //}
                //    //else if (int.TryParse(tb_cena.Text, out parse) || tb_cena.Text.Trim().Length > 0)
                //    //    tb_cena.BorderBrush = new SolidColorBrush(Colors.Black);

                //    if (rb_cena.IsChecked == true && (!int.TryParse(tb_cena.Text, out parse) || tb_cena.Text.Trim().Length == 0))
                //    {
                //        tb_cena.BorderBrush = new SolidColorBrush(Colors.Red);
                //        flag = 0;
                //    }
                //    else if (rb_cena.IsChecked == true && (int.TryParse(tb_cena.Text, out parse) || tb_cena.Text.Trim().Length > 0))
                //        tb_cena.BorderBrush = new SolidColorBrush(Colors.Black);

                //    if (dp_datum_konca.Text.Trim().Length == 0)
                //    {
                //        dp_datum_konca.BorderBrush = new SolidColorBrush(Colors.Red);
                //        flag = 0;
                //    }
                //    else if (dp_datum_konca.Text.Trim().Length > 0)
                //        dp_datum_konca.BorderBrush = new SolidColorBrush(Colors.Black);

                //    if (dp_datum_zacetka.Text.Trim().Length == 0)
                //    {
                //        dp_datum_zacetka.BorderBrush = new SolidColorBrush(Colors.Red);
                //        flag = 0;
                //    }
                //    else if (dp_datum_zacetka.Text.Trim().Length > 0)
                //        dp_datum_zacetka.BorderBrush = new SolidColorBrush(Colors.Black);
                //}

                if (lb_pd.SelectedItem != null)
                {
                    var connectionString = ConfigurationManager.ConnectionStrings["myDatabaseConnection"].ConnectionString;

                    MessageBoxResult result = MessageBox.Show("Ali želite oddati oglas?", "Oddaj Oglas", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                        using (MySqlConnection connection = new MySqlConnection(connectionString))
                        {
                            //Get proper int values from listbox vrsta_dela
                            List<int> selectedItemIndexes = new List<int>();
                            foreach (object i in lb_pd.SelectedItems)
                                selectedItemIndexes.Add(lb_pd.Items.IndexOf(i));
                            selectedItemIndexes.Sort();

                            connection.ConnectionString = connectionString;
                            var queryGetId = "select max(id_ponudbe)+1 from lista_ponudb;";
                            var getMaxIdVrstaDela = "select max(id)+1 from delo_mapping;";
                            var getMaxIdPriponka = "select max(idpriponke)+1 from priponke;";

                            var query = "INSERT INTO lista_ponudb (id_ponudbe, vrsta_dela, cena, datum_zacetka, datum_konca, ustvarjeno, opis, id_uporabnika, izbrisana_ponudba, dimenzije, teza, material, priponke) values (@IdPonudbe, @VrstaDela, @Cena, @DatumZacetka, @DatumKonca, @Ustvarjeno, @Opis, @IdUporabnika, @IzbrisanaPonudba, @Dimenzije, @Teza, @Material, @Priponke);";
                            var queryInsertVrstaDela = "insert into delo_mapping (id, vrste_dela_id) values (@IdVrstaDela, @SelectedItem);";
                            var queryInsertPriponke = "insert into priponke (idpriponke, priponka1, priponka2, priponka3, date, priponka1_name, priponka2_name, priponka3_name) values (@IdPriponke, @Priponka1, @Priponka2, @Priponka3, @DatumPriponke, @Priponka1Name, @Priponka2Name, @Priponka3Name)";

                            connection.Open();

                            MySqlCommand commandgetid = new MySqlCommand(queryGetId, connection);
                            int id_ponudbe = Convert.ToInt32(commandgetid.ExecuteScalar());

                            MySqlCommand commandgetmaxidvrstadela = new MySqlCommand(getMaxIdVrstaDela, connection);
                            int id_vrsta_dela = Convert.ToInt32(commandgetmaxidvrstadela.ExecuteScalar());

                            MySqlCommand commandgetmaxidpriponke = new MySqlCommand(getMaxIdPriponka, connection);
                            int id_priponke = Convert.ToInt32(commandgetmaxidpriponke.ExecuteScalar());

                        //int id_priponke2 = id_priponke;

                        // MySqlCommand commandInsertPriponke = new MySqlCommand(queryInsertPriponke, connection);
                        // commandInsertPriponke.Parameters.AddWithValue("@IdPriponke", id_priponke);
                        // if (fileName1 != "")
                        // {
                        //     commandInsertPriponke.Parameters.AddWithValue("@Priponka1", fileName1);
                        //     commandInsertPriponke.Parameters.AddWithValue("@Priponka1Name", fileName1link);
                        // }
                        // else { commandInsertPriponke.Parameters.AddWithValue("@Priponka1", null); commandInsertPriponke.Parameters.AddWithValue("@Priponka1Name", null); }
                        //if (fileName2 != "")
                        //     commandInsertPriponke.Parameters.AddWithValue("@Priponka2", fileName2);
                        // else commandInsertPriponke.Parameters.AddWithValue("@Priponka2", null);
                        // if (fileName3 != "")
                        //     commandInsertPriponke.Parameters.AddWithValue("@Priponka3", fileName3);
                        // else commandInsertPriponke.Parameters.AddWithValue("@Priponka3", null);
                        // commandInsertPriponke.Parameters.AddWithValue("@DatumPriponke", DateTime.Now);
                        // commandInsertPriponke.ExecuteNonQuery();

                        if (listBox_priponke.Items.Count > 0)
                        {
                            MySqlCommand commandInsertPriponke = new MySqlCommand(queryInsertPriponke, connection);
                            commandInsertPriponke.Parameters.AddWithValue("@IdPriponke", id_priponke);
                            commandInsertPriponke.Parameters.AddWithValue("@DatumPriponke", DateTime.Now);

                            if (listBox_priponke.Items.Count == 1)
                            {
                                commandInsertPriponke.Parameters.AddWithValue("@Priponka1", fileName[0]);
                                commandInsertPriponke.Parameters.AddWithValue("@Priponka1Name", fileNameLink[0]);
                                commandInsertPriponke.Parameters.AddWithValue("@Priponka2", null);
                                commandInsertPriponke.Parameters.AddWithValue("@Priponka2Name", null);
                                commandInsertPriponke.Parameters.AddWithValue("@Priponka3", null);
                                commandInsertPriponke.Parameters.AddWithValue("@Priponka3Name", null);
                            }
                            else if (listBox_priponke.Items.Count == 2)
                            {
                                commandInsertPriponke.Parameters.AddWithValue("@Priponka1", fileName[0]);
                                commandInsertPriponke.Parameters.AddWithValue("@Priponka1Name", fileNameLink[0]);
                                commandInsertPriponke.Parameters.AddWithValue("@Priponka2", fileName[1]);
                                commandInsertPriponke.Parameters.AddWithValue("@Priponka2Name", fileNameLink[1]);
                                commandInsertPriponke.Parameters.AddWithValue("@Priponka3", null);
                                commandInsertPriponke.Parameters.AddWithValue("@Priponka3Name", null);
                            }
                            else if (listBox_priponke.Items.Count == 3)
                            {
                                commandInsertPriponke.Parameters.AddWithValue("@Priponka1", fileName[0]);
                                commandInsertPriponke.Parameters.AddWithValue("@Priponka1Name", fileNameLink[0]);
                                commandInsertPriponke.Parameters.AddWithValue("@Priponka2", fileName[1]);
                                commandInsertPriponke.Parameters.AddWithValue("@Priponka2Name", fileNameLink[1]);
                                commandInsertPriponke.Parameters.AddWithValue("@Priponka3", fileName[2]);
                                commandInsertPriponke.Parameters.AddWithValue("@Priponka3Name", fileNameLink[2]);
                            }
                            commandInsertPriponke.ExecuteNonQuery();
                                //FTP Upload Priponke
                                UploadFTPPriponke();
                            }

                            

                            MySqlCommand command = new MySqlCommand(query, connection);
                            command.Parameters.AddWithValue("@IdPonudbe", id_ponudbe);
                            command.Parameters.AddWithValue("@VrstaDela", id_vrsta_dela);
                            if (rb_cena.IsEnabled == true)
                                command.Parameters.AddWithValue("@Cena", tb_cena.Text);
                            else if (rb_cena_podogovoru.IsEnabled == true)
                                command.Parameters.AddWithValue("@Cena", "Po Dogovoru");
                            command.Parameters.AddWithValue("@DatumZacetka", dp_datum_zacetka.SelectedDate);
                            command.Parameters.AddWithValue("@DatumKonca", dp_datum_konca.SelectedDate);
                            command.Parameters.AddWithValue("@Ustvarjeno", DateTime.Now);
                            command.Parameters.AddWithValue("@Opis", tb_opis.Text);
                            command.Parameters.AddWithValue("@IdUporabnika", (Application.Current as App).uporabnikId);
                            command.Parameters.AddWithValue("@IzbrisanaPonudba", 0);
                            command.Parameters.AddWithValue("@Dimenzije", tb_dimenzije.Text);
                            command.Parameters.AddWithValue("@Teza", tb_teza.Text);
                            command.Parameters.AddWithValue("@Material", tb_material.Text);
                            //command.Parameters.AddWithValue("@TipOglasa", 1);
                            //if (!(fileName1 == "" && fileName2 == "" & fileName3 == ""))
                            //    command.Parameters.AddWithValue("@Priponke", id_priponke);
                            if(listBox_priponke.Items.Count != 0)
                                command.Parameters.AddWithValue("@Priponke", id_priponke);
                            else command.Parameters.AddWithValue("@Priponke", null);
                            //if (fileName2 != "")
                            //{
                            //    command.Parameters.AddWithValue("@Priponka2", id_priponke2);
                            //    id_priponke2++;
                            //}
                            //else command.Parameters.AddWithValue("@Priponka2", null);
                            //if (fileName3 != "")
                            //{
                            //    command.Parameters.AddWithValue("@Priponka3", id_priponke2);
                            //    id_priponke2++;
                            //}
                            //else command.Parameters.AddWithValue("@Priponka3", null);

                            command.ExecuteNonQuery();


                            for (int i = 0; i < selectedItemIndexes.Count; i++)
                            {
                                MySqlCommand commandVrstaDela = new MySqlCommand(queryInsertVrstaDela, connection);

                                commandVrstaDela.Parameters.AddWithValue("@IdVrstaDela", id_vrsta_dela);
                                commandVrstaDela.Parameters.AddWithValue("@SelectedItem", selectedItemIndexes[i].ToString() + "a");
                                commandVrstaDela.ExecuteNonQuery();
                            }

                            connection.Close();
                        }
                }
            //}
            //catch (Exception ex)
            //{ MessageBox.Show(ex.Message); }

            //this.DialogResult = true;
        }

        

        //private void button1_Click(object sender, RoutedEventArgs e)
        //{
        //    var connectionString = ConfigurationManager.ConnectionStrings["myDatabaseConnection"].ConnectionString;

        //    //try
        //    {
        //        int flag = 0;

        //        if (listBox2.SelectedItem != null && tb_opis2.Text.Trim().Length > 0)
        //            flag = 1;
        //        else
        //        {
        //            if (listBox2.SelectedItem == null)
        //            {
        //                listBox2.BorderBrush = new SolidColorBrush(Colors.Red);
        //                flag = 0;
        //            }
        //            else if (listBox2.SelectedItem != null)
        //                listBox2.BorderBrush = new SolidColorBrush(Colors.Black);


        //            if (tb_opis2.Text.Trim().Length == 0)
        //            {
        //                tb_opis2.BorderBrush = new SolidColorBrush(Colors.Red);
        //                flag = 0;
        //            }
        //            else if (tb_opis2.Text.Trim().Length > 0)
        //                tb_opis2.BorderBrush = new SolidColorBrush(Colors.Black);
        //        }

        //        if (flag == 1)
        //        {
        //            MessageBoxResult result = MessageBox.Show("Ali želite oddati oglas?", "Oddaj Oglas", MessageBoxButton.YesNo);
        //            if (result == MessageBoxResult.Yes)
        //                using (MySqlConnection connection = new MySqlConnection(connectionString))
        //                {
        //                    List<int> selectedItemIndexes = new List<int>();
        //                    foreach (object i in listBox2.SelectedItems)
        //                        selectedItemIndexes.Add(listBox2.Items.IndexOf(i));
        //                    selectedItemIndexes.Sort();

        //                    connection.ConnectionString = connectionString;
        //                    var queryGetId = "select max(id_ponudbe)+1 from lista_ponudb;";
        //                    var getMaxIdVrstaDela = "select max(id)+1 from delo_mapping;";
        //                    var getMaxIdPriponka = "select max(id)+1 from priponke;";

        //                    var query = "INSERT INTO lista_ponudb (id_ponudbe, vrsta_dela, opis, id_uporabnika, izbrisana_ponudba, tip_oglasa, priponka1, priponka2, priponka3) values (@IdPonudbe, @VrstaDela, @Opis, @IdUporabnika, @IzbrisanaPonudba, @TipOglasa, @Priponka1, @Priponka2, @Priponka3);";
        //                    var queryInsertVrstaDela = "insert into delo_mapping (id, vrste_dela_id) values (@IdVrstaDela, @SelectedItem);";
        //                    var queryInsertPriponke = "insert into priponke (idpriponke, filename, date) values (@IdPriponke, @FileName, @DatumPriponke)";

        //                    connection.Open();

        //                    MySqlCommand commandgetid = new MySqlCommand(queryGetId, connection);
        //                    int id_ponudbe = Convert.ToInt32(commandgetid.ExecuteScalar());

        //                    MySqlCommand commandgetmaxidvrstadela = new MySqlCommand(getMaxIdVrstaDela, connection);
        //                    int id_vrsta_dela = Convert.ToInt32(commandgetmaxidvrstadela.ExecuteScalar());

        //                    MySqlCommand commandgetmaxidpriponke = new MySqlCommand(getMaxIdPriponka, connection);
        //                    int id_priponke = Convert.ToInt32(commandgetmaxidpriponke.ExecuteScalar());

        //                    MySqlCommand command = new MySqlCommand(query, connection);
        //                    command.Parameters.AddWithValue("@IdPonudbe", id_ponudbe);
        //                    command.Parameters.AddWithValue("@VrstaDela", id_vrsta_dela);
        //                    command.Parameters.AddWithValue("@Opis", tb_opis2.Text);
        //                    command.Parameters.AddWithValue("@IdUporabnika", (Application.Current as App).uporabnikId);
        //                    command.Parameters.AddWithValue("@IzbrisanaPonudba", 0);
        //                    command.Parameters.AddWithValue("@TipOglasa", 2);
        //                    if (fileName1 != "")
        //                        command.Parameters.AddWithValue("@Priponka1", fileName1);
        //                    else command.Parameters.AddWithValue("@Priponka1", null);
        //                    if (fileName2 != "")
        //                        command.Parameters.AddWithValue("@Priponka2", fileName1);
        //                    else command.Parameters.AddWithValue("@Priponka2", null);
        //                    if (fileName3 != "")
        //                        command.Parameters.AddWithValue("@Priponka3", fileName1);
        //                    else command.Parameters.AddWithValue("@Priponka3", null);

        //                    command.ExecuteNonQuery();

        //                    for (int i = 0; i < selectedItemIndexes.Count; i++)
        //                    {
        //                        MySqlCommand commandVrstaDela = new MySqlCommand(queryInsertVrstaDela, connection);

        //                        commandVrstaDela.Parameters.AddWithValue("@IdVrstaDela", id_vrsta_dela);
        //                        commandVrstaDela.Parameters.AddWithValue("@SelectedItem", selectedItemIndexes[i].ToString() + "b");
        //                        commandVrstaDela.ExecuteNonQuery();
        //                    }


        //                    if (fileName1 != "")
        //                    {
        //                        MySqlCommand commandInsertPriponke = new MySqlCommand(queryInsertPriponke, connection);
        //                        commandInsertPriponke.Parameters.AddWithValue("@IdPriponke", id_priponke);
        //                        commandInsertPriponke.Parameters.AddWithValue("@FileName", fileName1);
        //                        commandInsertPriponke.Parameters.AddWithValue("@DatumPriponke", DateTime.Now.ToShortDateString());
        //                        commandInsertPriponke.ExecuteNonQuery();
        //                        id_priponke++;
        //                    }
        //                    if (fileName2 != "")
        //                    {
        //                        MySqlCommand commandInsertPriponke = new MySqlCommand(queryInsertPriponke, connection);
        //                        commandInsertPriponke.Parameters.AddWithValue("@IdPriponke", id_priponke);
        //                        commandInsertPriponke.Parameters.AddWithValue("@FileName", fileName2);
        //                        commandInsertPriponke.Parameters.AddWithValue("@DatumPriponke", DateTime.Now.ToShortDateString());
        //                        commandInsertPriponke.ExecuteNonQuery();
        //                        id_priponke++;
        //                    }
        //                    if (fileName3 != "")
        //                    {
        //                        MySqlCommand commandInsertPriponke = new MySqlCommand(queryInsertPriponke, connection);
        //                        commandInsertPriponke.Parameters.AddWithValue("@IdPriponke", id_priponke);
        //                        commandInsertPriponke.Parameters.AddWithValue("@FileName", fileName3);
        //                        commandInsertPriponke.Parameters.AddWithValue("@DatumPriponke", DateTime.Now.ToShortDateString());
        //                        commandInsertPriponke.ExecuteNonQuery();
        //                    }


        //                    connection.Close();
        //                }
        //        }
        //    }
        //    //catch (Exception ex)
        //    //{ MessageBox.Show(ex.Message); }
        //}

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //listbox_selection = listbox_selection + " " + listBox.SelectedItem.ToString();
            //MessageBox.Show(listbox_selection);
        }

        //struct FtpSetting
        //{
        //    public string Server { get; set; }
        //    public string Username { get; set; }
        //    public string Password { get; set; }
        //    public string FileName { get; set; }
        //    public string FullName { get; set; }
        //}

        //FtpSetting _input_parameter;
        
        

        

        //private void worker_DoWork(object sender, DoWorkEventArgs e)
        private void UploadFTPPriponke()
        {
            //string active_fullname = "";
            //string active_filename = "";
            //string active_filename_link = "";
            //if (active_priponka == 1)
            //{
            //    active_filename = fileName1;
            //    active_fullname = fullName1;
            //    active_filename_link = fileName1link;
            //}
            //else if (active_priponka == 2)
            //{
            //    active_filename = fileName2;
            //    active_fullname = fullName2;
            //}
            //else if (active_priponka == 3)
            //{
            //    active_filename = fileName3;
            //    active_fullname = fullName3;
            //}

            for (int i = 0; i < listBox_priponke.Items.Count; i++)
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(string.Format("{0}/ftp/{1}", server, fileNameLink[i])));

                try
                {
                    request.Method = WebRequestMethods.Ftp.UploadFile;
                    request.Credentials = new NetworkCredential(userName, password);
                    Stream ftpStream = request.GetRequestStream();
                    FileStream fs = File.OpenRead(fullName[i]);
                    byte[] buffer = new byte[1024];
                    double total = (double)fs.Length;
                    int byteRead = 0;
                    //double read = 0;
                    do
                    {
                        if (!worker.CancellationPending)
                        {
                            byteRead = fs.Read(buffer, 0, 1024);
                            ftpStream.Write(buffer, 0, byteRead);
                            //read += (double)byteRead;
                            //double percentage = read / total * 100;
                            //worker.ReportProgress((int)percentage);

                        }
                    }
                    while (byteRead != 0);
                    fs.Close();
                    ftpStream.Close();
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }
            }
        }

        //private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{
        //    lbl_ime_priponke.Content = "Uploaded " + e.ProgressPercentage + "%";
        //    progress_upload.Value = e.ProgressPercentage;
        //    //progress_upload.UpdateLayout();
        //}

        //private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    //progress_upload.Visibility = Visibility.Hidden;
        //    if (active_priponka == 1)
        //    {
        //        lbl_ime_priponke.Content = "Ime datoteke: " + fileName1;
        //        btn_odstrani_priponko.Visibility = Visibility.Visible;
        //        btn_dodaj_priponko2.Visibility = Visibility.Visible;
        //    }
        //    else if (active_priponka == 2)
        //    {
        //        lbl_ime_priponke_2.Content = "Ime datoteke: " + fileName2;
        //        btn_odstrani_priponko2.Visibility = Visibility.Visible;
        //        btn_dodaj_priponko3.Visibility = Visibility.Visible;
        //    }
        //    else if (active_priponka == 3)
        //    {
        //        lbl_ime_priponke_3.Content = "Ime datoteke: " + fileName3;
        //        btn_odstrani_priponko3.Visibility = Visibility.Visible;
        //    }


        //    Mouse.OverrideCursor = null;
        //    //active_priponka++;
        //}

        private void btn_dodaj_priponko_Click(object sender, RoutedEventArgs e) //dodaj 1
        {
            if (listBox_priponke.Items.Count <= 2)
            {

                OpenFileDialog file_dialog = new OpenFileDialog() { Multiselect = false, ValidateNames = true, Filter = "All files (*.*)|*.*", Title = "Izberi datoteko" };
                if (file_dialog.ShowDialog() == true)
                {

                    Mouse.OverrideCursor = Cursors.Wait;
                    FileInfo fi = new FileInfo(file_dialog.FileName);

                    fileName.Add(fi.Name);
                    fullName.Add(fi.FullName);
                    fileNameLink.Add("Priponka_" + DateTime.Now);

                    //listBox_priponke.Visibility = Visibility.Visible;
                    listBox_priponke.Items.Add(fi.Name);
                }
                Mouse.OverrideCursor = null;
            }
            else MessageBox.Show("Maksimalno število priponk pri oglasu je 3");

            //for(int i = 0; i < listBox_priponke.Items.Count; i++)
            //{
            //    MessageBox.Show("filename: " + fileName[i] + ", fullname: " + fullName[i] + ", filenamelink: " + fileNameLink[i]);
            //}
        }

        private void btn_odstrani_priponko_Click(object sender, RoutedEventArgs e) //odstrani 1
        {
            //Še za stestirat
            if (listBox_priponke.SelectedItem != null)
            {
                fileName.RemoveAt(listBox_priponke.SelectedIndex);
                fullName.RemoveAt(listBox_priponke.SelectedIndex);
                fileNameLink.RemoveAt(listBox_priponke.SelectedIndex);
                listBox_priponke.Items.RemoveAt(listBox_priponke.SelectedIndex);
            }
            else MessageBox.Show("Izberi priponko, ki jo želiš odstraniti");

            //for (int i = 0; i < listBox_priponke.Items.Count; i++)
            //{
            //    MessageBox.Show("filename: " + fileName[i] + ", fullname: " + fullName[i] + ", filenamelink: " + fileNameLink[i]);
            //}
        }

        //private void btn_dodaj_priponko2_Click(object sender, RoutedEventArgs e) //dodaj 2
        //{
        //    active_priponka = 2;
        //    //DobiPriponko();
        //    if(DobiPriponko() == true)
        //        worker.RunWorkerAsync();
        //    else { MessageBox.Show("Napaka"); Mouse.OverrideCursor = null; }
        //}

        //private void btn_odstrani_priponko2_Click(object sender, RoutedEventArgs e) //odstrani 2
        //{
        //    fileName2 = "";
        //    lbl_ime_priponke_2.Content = "";
        //    btn_odstrani_priponko2.Visibility = Visibility.Hidden;
        //}

        //private void btn_dodaj_priponko3_Click(object sender, RoutedEventArgs e) //dodaj 3
        //{
        //    active_priponka = 3;
        //    //DobiPriponko();
        //    if (DobiPriponko() == true)
        //        worker.RunWorkerAsync();
        //    else { MessageBox.Show("Napaka"); Mouse.OverrideCursor = null; }
        //}

        //private void btn_odstrani_priponko3_Click(object sender, RoutedEventArgs e) //odstrani 3
        //{
        //    fileName3 = "";
        //    lbl_ime_priponke_3.Content = "";
        //    btn_odstrani_priponko3.Visibility = Visibility.Hidden;
        //}

        private bool DobiPriponko()
        {
            OpenFileDialog file_dialog = new OpenFileDialog() { Multiselect = false, ValidateNames = true, Filter = "All files (*.*)|*.*", Title = "Izberi datoteko" };

            //file_dialog.Filter = "All files (*.*)|*.*";
            //file_dialog.Title = "Izberi datoteko";


            if (file_dialog.ShowDialog() == true)
            {

                Mouse.OverrideCursor = Cursors.Wait;
                FileInfo fi = new FileInfo(file_dialog.FileName);

                fileName.Add(fi.Name);
                fullName.Add(fi.FullName);
                fileNameLink.Add("Priponka1_" + DateTime.Now);

                //if (fi.Length < 1000000)
                //{
                    //if (active_priponka == 1)
                    //{
                    //    fullName1 = fi.FullName;
                    //    fileName1 = fi.Name;
                    //    fileName1link = "Priponka1_" + DateTime.Now;
                        
                    //}
                    //else if (active_priponka == 2)
                    //{
                    //    fullName2 = fi.FullName;
                    //    fileName2 = fi.Name;
                    //}
                    //else if (active_priponka == 3)
                    //{
                    //    fullName3 = fi.FullName;
                    //    fileName3 = fi.Name;
                    //}

                    return true;
                //}

                //else
                //{  return false; }

                //progress_upload.Visibility = Visibility.Visible;
                //lbl_ime_priponke.Content = "";

            }
            else return false;
        }

        //private void rb_cena_podogovoru_Checked(object sender, RoutedEventArgs e)
        //{
        //    tb_cena.IsEnabled = false;
        //    tb_cena.Text = "PO DOGOVORU";
        //}

        //private void rb_cena_Checked(object sender, RoutedEventArgs e)
        //{
        //    tb_cena.IsEnabled = true;
        //    tb_cena.Text = "";
        //}


        private void rb_cena_podogovoru_Unchecked_1(object sender, RoutedEventArgs e)
        {
            tb_cena.IsEnabled = true;
            tb_cena.Text = "";
        }

        private void rb_cena_Unchecked_1(object sender, RoutedEventArgs e)
        {
            tb_cena.IsEnabled = false;
            tb_cena.Text = "";  
        }

        private void btn_oddaj_oglas_pk_Click(object sender, RoutedEventArgs e)
        {
            if (lb_pk.SelectedItem != null)
            {
                var connectionString = ConfigurationManager.ConnectionStrings["myDatabaseConnection"].ConnectionString;

                MessageBoxResult result = MessageBox.Show("Ali želite oddati oglas?", "Oddaj Oglas", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
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

                        var query = "INSERT INTO proste_kapacitete (id_proste_kapacitete, vrsta_dela, datum_zacetka, datum_konca, ustvarjeno, opis, id_uporabnika, izbrisana_ponudba) values (@IdPonudbe, @VrstaDela, @DatumZacetka, @DatumKonca, @Ustvarjeno, @Opis, @IdUporabnika, @IzbrisanaPonudba);";
                        var queryInsertVrstaDela = "insert into pk_mapping (id, vrste_strojev_id) values (@IdVrsteStrojev, @SelectedItem);";

                        connection.Open();

                        MySqlCommand commandgetid = new MySqlCommand(queryGetId, connection);
                        int id_ponudbe = Convert.ToInt32(commandgetid.ExecuteScalar());

                        MySqlCommand commandgetmaxidvrstadela = new MySqlCommand(getMaxIdVrstaDela, connection);
                        int id_vrsta_stroja = Convert.ToInt32(commandgetmaxidvrstadela.ExecuteScalar());


                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue("@IdPonudbe", id_ponudbe);
                        command.Parameters.AddWithValue("@VrstaDela", id_vrsta_stroja);
                        command.Parameters.AddWithValue("@DatumZacetka", dp_datum_zacetka_pk.SelectedDate);
                        command.Parameters.AddWithValue("@DatumKonca", dp_datum_konca_pk.SelectedDate);
                        command.Parameters.AddWithValue("@Ustvarjeno", DateTime.Now);
                        command.Parameters.AddWithValue("@Opis", tb_opis_pk.Text);
                        command.Parameters.AddWithValue("@IdUporabnika", (Application.Current as App).uporabnikId);
                        command.Parameters.AddWithValue("@IzbrisanaPonudba", 0);

                        command.ExecuteNonQuery();


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
        }
    }
}
