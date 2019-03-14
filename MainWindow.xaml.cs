using MySql.Data.MySqlClient;
using Orodjarne.Models;
using Orodjarne.ViewModels;
using Orodjarne.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
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
using System.Windows.Threading;

namespace Orodjarne
{
    public partial class MainWindow : Window
    {
        bool allow_close = false;
        int restart_timer = 1;

        public MainWindow(bool update_user_db_version)
        {
            InitializeComponent();

            if ((Application.Current as App).modul_lista_ponudb == 1)
            {
                btn.Visibility = Visibility.Visible;
                btn.IsChecked = true;
            }
            else btn.Visibility = Visibility.Collapsed;

            if ((Application.Current as App).modul_moji_oglasi == 1)
                btn4.Visibility = Visibility.Visible;
            else btn4.Visibility = Visibility.Collapsed;

            if ((Application.Current as App).modul_prevozi == 1)
            {
                btn7.Visibility = Visibility.Visible;
                btn7.IsChecked = true;
            }
            else btn7.Visibility = Visibility.Collapsed;

            if ((Application.Current as App).modul_sporocila == 1)
                btn2.Visibility = Visibility.Visible;
            else btn2.Visibility = Visibility.Collapsed;

            //lbl_aktivna_stran.Content = "LISTA PONUDB";
            if ((Application.Current as App).logo != null)
            {
                byte[] byteBlob = (Application.Current as App).logo as byte[];
                MemoryStream ms = new MemoryStream(byteBlob);
                BitmapImage bmi = new BitmapImage();
                bmi.BeginInit();
                bmi.StreamSource = ms;
                bmi.EndInit();
                image_box.Source = bmi;
            }
            //ContentGrid.Content = new ListaPonudb();
            lbl_uporabnik_ime.Content = "Prijavljen kot: " + (Application.Current as App).odgovorna_oseba.ToString();
            lbl_uporabnik_podjetje.Content = (Application.Current as App).podjetje.ToString();

            if(update_user_db_version == true)
                UpdateVersionDBUser();

            //DataContext = new NotificationViewModel();

            //testing
            //DispatcherTimer timer = new DispatcherTimer();
            //timer.Interval = TimeSpan.FromSeconds(restart_timer);
            //timer.Tick += timer_Tick;
            //timer.Start();


            //NotificationViewModel vm = new NotificationViewModel();
            //vm.Notification.NovoSporocilo = "novo";
            //vm.setnotification();
            //vm.setnotification();
            //UpdateNotification();
        }

        private void UpdateVersionDBUser()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["myDatabaseConnection"].ConnectionString;
            double new_app_version = double.Parse(ConfigurationManager.AppSettings["version"]);

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                var query = "update uporabniki set app_version = '" + new_app_version.ToString(CultureInfo.InvariantCulture) + "' where id = " + (Application.Current as App).uporabnikId + ";";
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.ExecuteScalar();
                }
                connection.Close();
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            ShowNotificationSporocilo();
            try
            {
                if ((Application.Current as App).novo_sporocilo == 1)
                    message_notification.Visibility = Visibility.Visible;
                else if ((Application.Current as App).novo_sporocilo == 0)
                    message_notification.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //private void btn_Click(object sender, RoutedEventArgs e)
        //{
        //    //btn.IsChecked = true;

        //    //if (btn.IsChecked == true)

        //    //    ContentGrid.Content = new ListaPonudb();

            
        //    //btn1.IsChecked = false;
        //    //btn2.IsChecked = false;
        //    //btn3.IsChecked = false;
        //    //btn4.IsChecked = false;
        //    //btn7.IsChecked = false;
        //    //lbl_aktivna_stran.Content = "LISTA PONUDB";
        //}

        ////private void Button_Click(object sender, RoutedEventArgs e) //Lista ponudb
        ////{
        ////    ContentGrid.Content = new ListaPonudb();
        ////}

        //private void Button_Click_1(object sender, RoutedEventArgs e) //Oddaj oglas
        //{
        //    //if(btn1.IsChecked != true)
        //    //    ContentGrid.Content = new OddajOglas();

        //    //btn1.IsChecked = true;
        //    //btn.IsChecked = false;
        //    //btn2.IsChecked = false;
        //    //btn3.IsChecked = false;
        //    //btn4.IsChecked = false;
        //    //btn7.IsChecked = false;
        //    //lbl_aktivna_stran.Content = "ODDAJ OGLAS";
        //}

        //private void Button_Click_2(object sender, RoutedEventArgs e) //Sporočila
        //{
        //    ContentGrid.Content = new Sporocila();
        //    btn2.IsChecked = true;
        //    btn1.IsChecked = false;
        //    btn.IsChecked = false;
        //    btn3.IsChecked = false;
        //    btn4.IsChecked = false;
        //    //btn7.IsChecked = false;
        //    //lbl_aktivna_stran.Content = "SPOROČILA";
        //}

        //private void Button_Click_3(object sender, RoutedEventArgs e) //Nastavitve
        //{
        //    ContentGrid.Content = new Nastavitve();
        //    btn3.IsChecked = true;
        //    btn1.IsChecked = false;
        //    btn2.IsChecked = false;
        //    btn.IsChecked = false;
        //    btn4.IsChecked = false;
        //    //btn7.IsChecked = false;
        //    //lbl_aktivna_stran.Content = "NASTAVITVE";
        //}

        //private void Button_Click_4(object sender, RoutedEventArgs e) //Moji Oglasi
        //{
        //    ContentGrid.Content = new MojiOglasi();
        //    btn4.IsChecked = true;
        //    btn1.IsChecked = false;
        //    btn2.IsChecked = false;
        //    btn3.IsChecked = false;
        //    btn.IsChecked = false;
        //    //btn7.IsChecked = false;
        //    //lbl_aktivna_stran.Content = "MOJI OGLASI";
        //}

        private void Button_Click_7(object sender, RoutedEventArgs e) //Prevozi
        {
            ContentGrid.Content = new Prevozi();
            //btn7.IsChecked = true;
            //btn1.IsChecked = false;
            btn2.IsChecked = false;
            btn3.IsChecked = false;
            btn.IsChecked = false;
            btn4.IsChecked = false;
            //lbl_aktivna_stran.Content = "MOJI OGLASI";
        }

        private void Button_Click_5(object sender, RoutedEventArgs e) //Odjava
        {
            MessageBoxResult result = MessageBox.Show("Ali se res želite odjaviti?", "Odjava", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                Login window = new Login();
                allow_close = true;
                this.Close();
                window.Show();
            }
        }

        private void Button_Click_6(object sender, RoutedEventArgs e) //Izhod
        {
            Application.Current.Shutdown();
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

        private void Window_Closed(object sender, EventArgs e)
        {
            
        }

        private void ShowNotificationSporocilo()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["myDatabaseConnection"].ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
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
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btn1_Checked(object sender, RoutedEventArgs e)
        {

            ContentGrid.Content = new OddajOglas();

        }

        private void btn_Checked(object sender, RoutedEventArgs e)
        {
            ContentGrid.Content = new ListaPonudb();
        }

        private void btn4_Checked(object sender, RoutedEventArgs e)
        {
            ContentGrid.Content = new MojiOglasi();
        }

        private void btn2_Checked(object sender, RoutedEventArgs e)
        {
            ContentGrid.Content = new Sporocila();
        }

        private void btn3_Checked(object sender, RoutedEventArgs e)
        {
            ContentGrid.Content = new Nastavitve();
        }

        private void btn7_Checked(object sender, RoutedEventArgs e)
        {
            ContentGrid.Content = new Prevozi();
        }
    }
}
