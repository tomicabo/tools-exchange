using Microsoft.Win32;
using MySql.Data.MySqlClient;
using Orodjarne.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
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
    /// Interaction logic for Sporocila.xaml
    /// </summary>
    public partial class Sporocila : UserControl
    {
        private int id_pogovora, prebran_pogovor, posiljatelj, prejemnik;
        int id_uporabnika = (Application.Current as App).uporabnikId;

        List<string> priponka_name = new List<string>();
        List<string> lista_ponudb = new List<string>(); 
        //string connectionString = "server = localhost; user = root; database = test_database; port = 3306; password = root123";
        //string connectionString = "server = sql11.freemysqlhosting.net; user = sql11201745; database = sql11201745; port = 3306; password = F7q23PrS1r";


        public Sporocila()
        {
            InitializeComponent();
            list_box_pogovori.DataContext = new PogovoriViewModel();
            if (list_box_pogovori.ItemsSource != null)
                list_box_pogovori.SelectedIndex = 0;
        }

        private void list_box_pogovori_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lista_ponudb.Clear();
            listBox_priponke.Items.Clear();

            int selected_index;
            //try
            //{
                if (list_box_pogovori.SelectedIndex != -1)
                {
                    selected_index = list_box_pogovori.SelectedIndex;

                    id_pogovora = int.Parse(lbl_id_pogovora.Content.ToString());
                    prebran_pogovor = int.Parse(lbl_prebran_pogovor.Content.ToString());
                    posiljatelj = int.Parse(lbl_posiljatelj.Content.ToString());
                    prejemnik = int.Parse(lbl_prejemnik.Content.ToString());

                    if (prebran_pogovor == 1)
                    {
                        UpdatePrebranPogovor(id_pogovora, posiljatelj, prejemnik);
                        ShowNotificationSporocilo();
                        list_box_pogovori.SelectedIndex = selected_index;
                    }
                }
                list_box_sporocila.DataContext = new SporocilaViewModel(id_pogovora);
                list_box_sporocila.SelectedIndex = list_box_sporocila.Items.Count - 1;
                list_box_sporocila.ScrollIntoView(list_box_sporocila.SelectedItem);

                DobiPriponke(id_pogovora);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["myDatabaseConnection"].ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                var queryGetId = "select max(idsporocila)+1 from sporocila;";
                var query = "insert into sporocila (idsporocila, ustvarjeno, posiljatelj, sporocilo, idpogovora, priponka) values (@IdSporocila, @Ustvarjeno, @Posiljatelj, @Sporocilo, @IdPogovora, @Priponka);";
                connection.Open();

                try
                {
                    MySqlCommand commandgetid_sporocila = new MySqlCommand(queryGetId, connection);
                    //if ((commandgetid_sporocila.ExecuteScalar() != null))
                    int id_sporocila = Convert.ToInt32(commandgetid_sporocila.ExecuteScalar());
                    //else id_sporocila = 1;

                    MySqlCommand command_sporocila = new MySqlCommand(query, connection);
                    command_sporocila.Parameters.AddWithValue("@IdSporocila", id_sporocila);
                    command_sporocila.Parameters.AddWithValue("@Ustvarjeno", DateTime.Now);
                    command_sporocila.Parameters.AddWithValue("@Posiljatelj", id_uporabnika);
                    command_sporocila.Parameters.AddWithValue("@Sporocilo", textBox.Text);
                    command_sporocila.Parameters.AddWithValue("@IdPogovora", id_pogovora);
                    command_sporocila.Parameters.AddWithValue("@Priponka", null);

                    command_sporocila.ExecuteNonQuery();

                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }
            }
            UpdateNeprebranPogovor();
            list_box_sporocila.DataContext = new SporocilaViewModel(id_pogovora);
            list_box_sporocila.SelectedIndex = list_box_sporocila.Items.Count - 1;
            list_box_sporocila.ScrollIntoView(list_box_sporocila.SelectedItem);
            textBox.Text = "";
        }

        private void button_Click2(object sender, RoutedEventArgs e)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["myDatabaseConnection"].ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                //var query = "insert into sporocila (idsporocila, ustvarjeno, posiljatelj, sporocilo, novo_sporocilo, idpogovora) values (@IdSporocila, @Ustvarjeno, @Posiljatelj, @Sporocilo, @NovoSporocilo, @IdPogovora);";
                //connection.Open();
                MessageBoxResult result = MessageBox.Show("Ali res želite izbrisati sporočilo?", "Izbriši sporočilo", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        connection.Open();
                        MySqlCommand commandGetPosiljatelj = new MySqlCommand("select posiljatelj from pogovori where idpogovori = " + id_pogovora + ";", connection);
                        int posiljatelj = Convert.ToInt32(commandGetPosiljatelj.ExecuteScalar());

                        MySqlCommand commandGetPrejemnik = new MySqlCommand("select prejemnik from pogovori where idpogovori = " + id_pogovora + ";", connection);
                        int prejemnik = Convert.ToInt32(commandGetPrejemnik.ExecuteScalar());

                        if (posiljatelj == id_uporabnika)
                        {
                            MySqlCommand command = new MySqlCommand("update pogovori set posiljatelj_izbrisano = " + 1 + " where idpogovori = " + id_pogovora + ";", connection);
                                command.ExecuteNonQuery();
                            //command.Parameters.AddWithValue("@Value", 1);
                        }

                        if (prejemnik == id_uporabnika)
                        {
                            MySqlCommand command2 = new MySqlCommand("update pogovori set prejemnik_izbrisano = " + 1 + " where idpogovori = " + id_pogovora + ";", connection);
                                command2.ExecuteNonQuery();
                            //command2.Parameters.AddWithValue("@Value", 1);
                        }
                    }
                    catch (Exception ex)
                    { MessageBox.Show(ex.Message); }
                    connection.Close();

                    list_box_pogovori.DataContext = new PogovoriViewModel();
                    list_box_pogovori.SelectedIndex = -1;
                    list_box_sporocila.DataContext = null;
                }
            }
        }

        private void button2_Copy_Click(object sender, RoutedEventArgs e)
        {
            //NotificationViewModel nvm = new NotificationViewModel();
            //nvm.Notification.NovoSporocilo = "222";
            //MainWindow mw = new MainWindow();
            //mw.DataContext = new NotificationViewModel();
        }

        private void button_priponka_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog file_dialog = new OpenFileDialog() { Multiselect = false, ValidateNames = true, Filter = "All files (*.*)|*.*", Title = "Izberi datoteko" };
            string fileName1link = "Priponka1_" + DateTime.Now;

            if (file_dialog.ShowDialog() == true)
            {
                FileInfo fi = new FileInfo(file_dialog.FileName);

                MessageBoxResult result = MessageBox.Show("Priponka " + fi.Name + " bo dodana k sporočilu.", "Priponka", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    //DATABASE ACTION
                    var connectionString = ConfigurationManager.ConnectionStrings["myDatabaseConnection"].ConnectionString;

                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        //INSERT PRIPONKA
                        var getMaxIdPriponka = "select max(idpriponke)+1 from priponke;";
                        var queryInsertPriponke = "insert into priponke (idpriponke, priponka1, priponka2, priponka3, date, priponka1_name) values (@IdPriponke, @Priponka1, @Priponka2, @Priponka3, @DatumPriponke, @Priponka1Name)";
                        

                        try
                        {
                            connection.Open();

                            MySqlCommand commandgetmaxidpriponke = new MySqlCommand(getMaxIdPriponka, connection);
                            int id_priponke = Convert.ToInt32(commandgetmaxidpriponke.ExecuteScalar());

                            MySqlCommand commandInsertPriponke = new MySqlCommand(queryInsertPriponke, connection);
                            commandInsertPriponke.Parameters.AddWithValue("@IdPriponke", id_priponke);
                            if (fi.Name != "")
                            {
                                commandInsertPriponke.Parameters.AddWithValue("@Priponka1", fi.Name);
                                commandInsertPriponke.Parameters.AddWithValue("@Priponka1Name", fileName1link);
                            }
                            commandInsertPriponke.Parameters.AddWithValue("@Priponka2", null);
                            commandInsertPriponke.Parameters.AddWithValue("@Priponka3", null);
                            commandInsertPriponke.Parameters.AddWithValue("@DatumPriponke", DateTime.Now);
                            commandInsertPriponke.ExecuteNonQuery();

                            //INSERT SPOROČILO
                            var queryGetId = "select max(idsporocila)+1 from sporocila;";
                            var query = "insert into sporocila (idsporocila, ustvarjeno, posiljatelj, sporocilo, idpogovora, priponka) values (@IdSporocila, @Ustvarjeno, @Posiljatelj, @Sporocilo, @IdPogovora, @Priponka);";

                            MySqlCommand commandgetid_sporocila = new MySqlCommand(queryGetId, connection);
                            int id_sporocila = Convert.ToInt32(commandgetid_sporocila.ExecuteScalar());

                            MySqlCommand command_sporocila = new MySqlCommand(query, connection);
                            command_sporocila.Parameters.AddWithValue("@IdSporocila", id_sporocila);
                            command_sporocila.Parameters.AddWithValue("@Ustvarjeno", DateTime.Now);
                            command_sporocila.Parameters.AddWithValue("@Posiljatelj", id_uporabnika);
                            command_sporocila.Parameters.AddWithValue("@Sporocilo", "Priponka: " + fi.Name);
                            command_sporocila.Parameters.AddWithValue("@IdPogovora", id_pogovora);
                            command_sporocila.Parameters.AddWithValue("@Priponka", id_priponke);

                            

                            command_sporocila.ExecuteNonQuery();

                            connection.Close();
                        }
                        catch (Exception ex)
                        { MessageBox.Show(ex.Message); }
                    }

                    //FTP ACTION
                    UploadFileFTP(fi.FullName, fileName1link);

                    UpdateNeprebranPogovor();
                    list_box_sporocila.DataContext = new SporocilaViewModel(id_pogovora);
                    list_box_sporocila.SelectedIndex = list_box_sporocila.Items.Count - 1;
                    list_box_sporocila.ScrollIntoView(list_box_sporocila.SelectedItem);
                    textBox.Text = "";
                }
            }
        }

        private void UpdatePrebranPogovor(int id, int pos, int prej)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["myDatabaseConnection"].ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    if(pos == id_uporabnika)
                    {
                        MySqlCommand command = new MySqlCommand("update pogovori set nov_pogovor_posiljatelj = " + 0 + " where idpogovori = " + id + ";", connection);
                        command.ExecuteNonQuery();
                    }
                    if (prej == id_uporabnika)
                    {
                        MySqlCommand command = new MySqlCommand("update pogovori set nov_pogovor_prejemnik = " + 0 + " where idpogovori = " + id + ";", connection);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }
                connection.Close();

                list_box_pogovori.DataContext = new PogovoriViewModel();
            }
        }

        private void UpdateNeprebranPogovor()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["myDatabaseConnection"].ConnectionString;

            DateTime time = DateTime.Now;              // Use current time
            string format = "yyyy-MM-dd HH:mm:ss";    // modify the format depending upon input required in the column in database 

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    if (posiljatelj == id_uporabnika)
                    {
                        MySqlCommand command = new MySqlCommand("update pogovori set nov_pogovor_prejemnik = " + 1 + ", zadnje_sporocilo = '" + time.ToString(format) + "' where idpogovori = " + id_pogovora + ";", connection);
                        command.ExecuteNonQuery();
                    }
                    if (prejemnik == id_uporabnika)
                    {
                        MySqlCommand command = new MySqlCommand("update pogovori set nov_pogovor_posiljatelj = " + 1 + ", zadnje_sporocilo = '" + time.ToString(format) + "' where idpogovori = " + id_pogovora + ";", connection);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }
                connection.Close();
            }
        }

        private void ShowNotificationSporocilo()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["myDatabaseConnection"].ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                var query = "SELECT IF(COUNT(*) > 0, TRUE, FALSE) AS result " +
                        "FROM pogovori " +
                        "where(posiljatelj = " + (Application.Current as App).uporabnikId + " and nov_pogovor_posiljatelj = 1) or " +
                        "(prejemnik = " + (Application.Current as App).uporabnikId + " and nov_pogovor_prejemnik = 1);";

                connection.Open();

                using (MySqlCommand commandgetid_sporocila = new MySqlCommand(query, connection))
                {
                    int novo_sporocilo = Convert.ToInt32(commandgetid_sporocila.ExecuteScalar());

                    if (novo_sporocilo == 1)
                        (Application.Current as App).novo_sporocilo = 1;
                    else (Application.Current as App).novo_sporocilo = 0;
                }

                connection.Close();
            }
        }

        private void btn_download_priponka_Click(object sender, RoutedEventArgs e)
        {
            //DownloadPriponka(listBox_priponke.SelectedItem.ToString());
            //MessageBox.Show(listBox_priponke.SelectedItem.ToString());

            for (int i = 0; i < lista_ponudb.Count; i++)
            {
                if (listBox_priponke.SelectedIndex == i)
                    DownloadPriponka(lista_ponudb[i], listBox_priponke.SelectedItem.ToString());
            }

            
        }

        private void DobiPriponke(int idpogovora)
        {


            var connectionString = ConfigurationManager.ConnectionStrings["myDatabaseConnection"].ConnectionString;
            string query = "select priponka1, priponka1_name from sporocila join priponke p on p.idpriponke = sporocila.priponka where idpogovora = " + idpogovora + " and priponka != 0;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(query, connection))
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //priponke_temp.IdPriponke = Convert.ToInt32(reader["idpriponke"]);
                        if (reader["priponka1"] != DBNull.Value)
                        {
                            //priponka_name.Add(reader["priponka1"].ToString());
                            listBox_priponke.Items.Add(reader["priponka1"]);
                        }
                        if (reader["priponka1_name"] != DBNull.Value)
                        {
                            lista_ponudb.Add(reader["priponka1_name"].ToString());
                        }
                    }
                }
                connection.Close();
            }
        }

        private void UploadFileFTP(string full_name, string filename_link)
        {
            string userName = "test";
            string password = "test";
            string server = "ftp://192.168.1.125";

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(string.Format("{0}/ftp/{1}", server, filename_link)));

            try
            {
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(userName, password);
                Stream ftpStream = request.GetRequestStream();
                FileStream fs = File.OpenRead(full_name);
                byte[] buffer = new byte[1024];
                double total = (double)fs.Length;
                int byteRead = 0;
                //double read = 0;
                do
                {
                    byteRead = fs.Read(buffer, 0, 1024);
                    ftpStream.Write(buffer, 0, byteRead);
                }
                while (byteRead != 0);
                fs.Close();
                ftpStream.Close();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void btn_priponka_download_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void DownloadPriponka(string filename, string userFile_name)
        {
            string server = "192.168.1.125:21";
            //string inputfilepath = @"C:\" + filename;
            //string ftphost = "xxx.xx.x.xxx";
            //string ftpfilepath = "/Updater/Dir1/FileName.exe";

            string ftpfullpath = "ftp://" + server + "/ftp/" + filename;

            //Stream myStream;
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "All files (*.*)|*.*";
            sfd.FileName = userFile_name;
            //sfd.FilterIndex = 2;
            sfd.RestoreDirectory = true;

            if (sfd.ShowDialog() == true)
            {
                using (WebClient request = new WebClient())
                {
                    //try
                    //{
                        request.Proxy = null;
                        request.Credentials = new NetworkCredential("test", "test");
                        byte[] fileData = request.DownloadData(ftpfullpath);

                        using (FileStream file = File.Create(sfd.FileName))
                        {
                            file.Write(fileData, 0, fileData.Length);
                            file.Close();
                        }
                        MessageBox.Show("Download Complete");
                    //}
                    //catch (Exception ex)
                    //{
                    //    MessageBox.Show(ex.Message);
                    //}
                }
            }


        }
    }
}
