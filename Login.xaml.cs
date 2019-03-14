using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
using Orodjarne.Models;
using System.IO;
using System.Collections.ObjectModel;
using Orodjarne.ViewModels;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using Microsoft.Win32;
using System.Diagnostics;

namespace Orodjarne
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        bool can_login = false;
        bool allow_close = false;
        //string connectionString = "server = localhost; user = root; database = test_database; port = 3306; password = root123";
        //string connectionString = "server = sql11.freemysqlhosting.net; user = sql11201745; database = sql11201745; port = 3306; password = F7q23PrS1r";
        ObservableCollection<UporabnikModel> uporabnik = new ObservableCollection<UporabnikModel>();

        public Login()
        {
            InitializeComponent();


            DobiUporabnika();
            
            tb_uporabnik.Focus();

            (Application.Current as App).uporabnikId = 0;
            (Application.Current as App).podjetje = "";
            (Application.Current as App).odgovorna_oseba = "";
            (Application.Current as App).uporabnisko_ime = "";
            (Application.Current as App).geslo = "";
            (Application.Current as App).tel_st = "";
            (Application.Current as App).email = "";
            (Application.Current as App).logo = null;
            (Application.Current as App).modul_lista_ponudb = 0;
            (Application.Current as App).modul_moji_oglasi = 0;
            (Application.Current as App).modul_prevozi = 0;
            (Application.Current as App).modul_sporocila = 0;
        }

        private ObservableCollection<UporabnikModel> DobiUporabnika()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["myDatabaseConnection"].ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                var query = "SELECT * FROM uporabniki";
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(query, connection))
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        UporabnikModel temp_uporabnik = new UporabnikModel();
                        temp_uporabnik.Id = Convert.ToInt32(reader["id"]);
                        temp_uporabnik.Podjetje = Convert.ToString(reader["podjetje"]);
                        temp_uporabnik.OdgovornaOseba = Convert.ToString(reader["odgovorna_oseba"]);
                        temp_uporabnik.UporabniskoIme = Convert.ToString(reader["uporabnisko_ime"]);
                        temp_uporabnik.Geslo = Convert.ToString(reader["geslo"]);
                        temp_uporabnik.Aktiven = Convert.ToInt32(reader["aktiven"]);
                        temp_uporabnik.TelSt = Convert.ToString(reader["tel_st"]);
                        temp_uporabnik.Email = Convert.ToString(reader["email"]);
                        temp_uporabnik.ModulListaPonudb = Convert.ToInt32(reader["modul_lista_ponudb"]);
                        temp_uporabnik.ModulMojiOglasi = Convert.ToInt32(reader["modul_moji_oglasi"]);
                        temp_uporabnik.ModulPrevozi = Convert.ToInt32(reader["modul_prevozi"]);
                        temp_uporabnik.ModulSporocila = Convert.ToInt32(reader["modul_sporocila"]);
                        if (reader["logo"] != DBNull.Value)
                        {
                            byte[] blob = reader["logo"] as byte[];

                            using (MemoryStream ms = new MemoryStream())
                            {
                                ms.Write(blob, 0, blob.Length);
                                Bitmap bm = (Bitmap)System.Drawing.Image.FromStream(ms);
                                using (MemoryStream msJpg = new MemoryStream())
                                {
                                    bm.Save(msJpg, ImageFormat.Jpeg);
                                    temp_uporabnik.Logo = msJpg.GetBuffer();
                                }
                            }
                        }

                        uporabnik.Add(temp_uporabnik);

                    }
                }
                connection.Close();
                return uporabnik;
            }
        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            if (tb_uporabnik.Text.Length == 0 || tb_geslo.Password.Length == 0)
            {
                lbl_napaka.Text = "Vnesite uporabniško ime in geslo.";

                progress_bar.Visibility = Visibility.Collapsed;
                progress_bar.IsIndeterminate = false;

                tb_uporabnik.Focus();
            }

            else
            {
                lbl_napaka.Text = "";

                progress_bar.Visibility = Visibility.Visible;
                progress_bar.IsIndeterminate = true;

                PreveriUporabnika();
                
            }
        }

        private void PreveriUporabnika()
        {
            bool update_user_db_version = false;
            foreach (var y in uporabnik)
            {
                if (tb_uporabnik.Text == y.UporabniskoIme && tb_geslo.Password == y.Geslo && y.Aktiven != 0)
                {
                    (Application.Current as App).uporabnikId = y.Id;
                    (Application.Current as App).podjetje = y.Podjetje;
                    (Application.Current as App).odgovorna_oseba = y.OdgovornaOseba;
                    (Application.Current as App).uporabnisko_ime = y.UporabniskoIme;
                    (Application.Current as App).geslo = y.Geslo;
                    (Application.Current as App).tel_st = y.TelSt;
                    (Application.Current as App).email = y.Email;
                    (Application.Current as App).logo = y.Logo;
                    (Application.Current as App).modul_lista_ponudb = y.ModulListaPonudb;
                    (Application.Current as App).modul_moji_oglasi = y.ModulMojiOglasi;
                    (Application.Current as App).modul_prevozi = y.ModulPrevozi;
                    (Application.Current as App).modul_sporocila = y.ModulSporocila;

                    can_login = true;
                }
            }

            if(CheckUpdate() == true)
            {
                update_user_db_version = true;
            }

            if (can_login == true)
            {
                lbl_napaka.Text = "";
                MainWindow window = new MainWindow(update_user_db_version);
                allow_close = true;
                this.Close();
                window.Show();
            }

            else
            {
                progress_bar.Visibility = Visibility.Collapsed;
                progress_bar.IsIndeterminate = false;

                lbl_napaka.Text = "Uporabniško ime ali geslo ni pravilno.";
            }

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (allow_close == false)
            {
                MessageBoxResult result = MessageBox.Show("Ali res želite zapreti program?", "Izhod", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    e.Cancel = false;
                }
                else if (result == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
            }
            else
                e.Cancel = false;
        }

        private void btn_quit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool CheckUpdate()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["myDatabaseConnection"].ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                double version_app;
                double version_user;
                //double current_version = double.Parse(ConfigurationManager.AppSettings["version"]);

                var query = "SELECT version FROM check_update";
                var query_user_version = "SELECT app_version FROM uporabniki where id = " + (Application.Current as App).uporabnikId + ";";

                connection.Open();

                //MySqlCommand command1 = new MySqlCommand(query, connection);

                using (MySqlCommand command = new MySqlCommand(query, connection))
                    version_app = Convert.ToDouble(command.ExecuteScalar());
                using (MySqlCommand command = new MySqlCommand(query_user_version, connection))
                    version_user = Convert.ToDouble(command.ExecuteScalar());

                if (version_app > version_user)
                {
                    MessageBoxResult result = MessageBox.Show("Na voljo je nova verzija programa. Ali jo želite prenesti sedaj?", "Nadgradnja verzije", MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.Yes)
                    {
                        //MessageBox.Show("Sprogramjri da bo naložilu novo verzijo");
                        DownloadInstaller();
                        this.Close();
                        return true;
                    }
                    return false;
                }
                connection.Close();
                return false;
            }
        }

        private void DownloadInstaller()
        {
            string server = "192.168.1.125:21";
            //string inputfilepath = @"C:\" + filename;
            //string ftphost = "xxx.xx.x.xxx";
            //string ftpfilepath = "/Updater/Dir1/FileName.exe";

            string ftpfullpath = "ftp://" + server + "/ftp/setup.exe";

            //Stream myStream;
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "All files (*.*)|*.*";
            sfd.FileName = "setup.exe";
            ////sfd.FilterIndex = 2;
            sfd.RestoreDirectory = true;

            if (sfd.ShowDialog() == true)
            {
                using (WebClient request = new WebClient())
                {
                    try
                    {
                        request.Proxy = null;
                        request.Credentials = new NetworkCredential("test", "test");
                        byte[] fileData = request.DownloadData(ftpfullpath);

                        using (FileStream file = File.Create(sfd.FileName))
                        {
                            file.Write(fileData, 0, fileData.Length);
                            file.Close();
                            Process.Start(sfd.FileName);
                        }
                        //MessageBox.Show("Download Complete");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }


        }
    }
}
