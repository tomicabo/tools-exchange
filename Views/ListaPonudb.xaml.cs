using Orodjarne.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Orodjarne.ViewModels;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Net;
using System.IO;
using Microsoft.Win32;

namespace Orodjarne.Views
{
    /// <summary>
    /// Interaction logic for ListaPonudb.xaml
    /// </summary>
    public partial class ListaPonudb : UserControl
    {
        int restart_timer = 100;
        private string vrsta_dela, opis, podjetje, odgovorna_oseba, tel_st, email, cena;
        private int id_ponudbe;
        DateTime datum_zacetka;
        DateTime datum_konca;
        private int id_uporabnika;
        //int IdPrejemnika;
        int id_pogovora;
        int id_sporocila;

        int priponke;
        string priponka1 = "";
        string priponka2 = "";
        string priponka3 = "";
        string priponka1_name = "";
        string priponka2_name = "";
        string priponka3_name = "";
        //string connectionString = "server = localhost; user = root; database = test_database; port = 3306; password = root123";
        //string connectionString = "server = sql11.freemysqlhosting.net; user = sql11201745; database = sql11201745; port = 3306; password = F7q23PrS1r";



        int selected_combo = 0;

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            RefreshGrid3("", 0);

            string filter_query = "";
            List<int> selectedItemIndexes = new List<int>();

            //if (comboBox.SelectedIndex == 1) //prosta dela - filter a
            if (btn_vrsta_prosta_dela.IsChecked == true)
            {
                if (lb_vrste_dela.SelectedItem != null)
                {
                    foreach (object i in lb_vrste_dela.SelectedItems)
                        selectedItemIndexes.Add(lb_vrste_dela.Items.IndexOf(i));
                    selectedItemIndexes.Sort();
                    filter_query = string.Join("a','", selectedItemIndexes);
                    list_ponudbe.DataContext = new ListaPonudbViewModelPDFilter(filter_query);
                }
                else
                    list_ponudbe.DataContext = new ListaPonudbViewModelPDFilter("");
            }

            //else if (comboBox.SelectedIndex == 2) //prosta dela - filter b
            else if (btn_vrsta_proste_kapacitete.IsChecked == true)
            {
                if (lb_vrste_strojev.SelectedItem != null)
                {
                    foreach (object i in lb_vrste_strojev.SelectedItems)
                        selectedItemIndexes.Add(lb_vrste_strojev.Items.IndexOf(i));
                    selectedItemIndexes.Sort();
                    filter_query = string.Join("','", selectedItemIndexes);
                    list_ponudbe.DataContext = new ListaPonudbViewModelPK(filter_query);
                }
                else
                    list_ponudbe.DataContext = new ListaPonudbViewModelPK("");
            }

            Mouse.OverrideCursor = null;

            //MessageBox.Show(filter_query);
        }

        private void btn_sporocilo_Click(object sender, RoutedEventArgs e)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["myDatabaseConnection"].ConnectionString;

            int result = 0;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                //Insert pogovori
                var queryGetId_pogovori = "select max(idpogovori)+1 from pogovori;";
                var query_pogovori = "insert into pogovori (idpogovori, ustvarjeno, zadnje_sporocilo, zadeva, posiljatelj, prejemnik, nov_pogovor_posiljatelj, nov_pogovor_prejemnik, posiljatelj_izbrisano, prejemnik_izbrisano) values (@IdPogovori, @Ustvarjeno, @ZadnjeSporocilo, @Zadeva, @Posiljatelj, @Prejemnik, @NovPogovorPosiljatelj, @NovPogovorPrejemnik, @PosiljateljIzbrisano, @PrejemnikIzbrisano);";
                connection.Open();

                try
                {
                    MySqlCommand commandgetid_pogovori = new MySqlCommand(queryGetId_pogovori, connection);
                    //if ((commandgetid_pogovori.ExecuteScalar() != null))
                    id_pogovora = Convert.ToInt32(commandgetid_pogovori.ExecuteScalar());
                    //else id_pogovora = 1;

                    MySqlCommand command_pogovori = new MySqlCommand(query_pogovori, connection);
                    command_pogovori.Parameters.AddWithValue("@IdPogovori", id_pogovora);
                    command_pogovori.Parameters.AddWithValue("@Ustvarjeno", DateTime.Now);
                    command_pogovori.Parameters.AddWithValue("@ZadnjeSporocilo", DateTime.Now);
                    command_pogovori.Parameters.AddWithValue("@Zadeva", vrsta_dela);
                    command_pogovori.Parameters.AddWithValue("@Posiljatelj", (Application.Current as App).uporabnikId);
                    command_pogovori.Parameters.AddWithValue("@Prejemnik", id_uporabnika);
                    command_pogovori.Parameters.AddWithValue("@NovPogovorPosiljatelj", 0);
                    command_pogovori.Parameters.AddWithValue("@NovPogovorPrejemnik", 1);
                    command_pogovori.Parameters.AddWithValue("@PosiljateljIzbrisano", 0);
                    command_pogovori.Parameters.AddWithValue("@PrejemnikIzbrisano", 0);

                    command_pogovori.ExecuteNonQuery();
                    result++;

                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }

                //Insert sporocilo
                var queryGetId_sporocila = "select max(idsporocila)+1 from sporocila;";
                var query_sporocila = "insert into sporocila (idsporocila, ustvarjeno, posiljatelj, sporocilo, idpogovora) values (@IdSporocila, @Ustvarjeno, @Posiljatelj, @Sporocilo, @IdPogovora);";

                try
                {
                    MySqlCommand commandgetid_sporocila = new MySqlCommand(queryGetId_sporocila, connection);
                    //if ((commandgetid_sporocila.ExecuteScalar() != null))
                    id_sporocila = Convert.ToInt32(commandgetid_sporocila.ExecuteScalar());
                    //else id_sporocila = 1;

                    MySqlCommand command_sporocila = new MySqlCommand(query_sporocila, connection);
                    command_sporocila.Parameters.AddWithValue("@IdSporocila", id_sporocila);
                    command_sporocila.Parameters.AddWithValue("@Ustvarjeno", DateTime.Now);
                    command_sporocila.Parameters.AddWithValue("@Posiljatelj", (Application.Current as App).uporabnikId);
                    command_sporocila.Parameters.AddWithValue("@Sporocilo", tb_sporocilo.Text);
                    command_sporocila.Parameters.AddWithValue("@IdPogovora", id_pogovora);

                    command_sporocila.ExecuteNonQuery();
                    result++;

                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }
            }

            if (result == 2)
            {
                MessageBox.Show("Sporočilo je bilo uspešno poslano");
                //this.Close();
            }
            else
                MessageBox.Show("Napaka");
        }


        public ListaPonudb()
        {
            InitializeComponent();
            //Mouse.OverrideCursor = Cursors.Wait;

            btn_vrsta_prosta_dela.IsChecked = true;

            
            //list_ponudbe.DataContext = new ListaPonudbViewModelPDFilter("");
            //Mouse.OverrideCursor = null;
            //comboBox.SelectedIndex = 0;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(restart_timer);
            timer.Tick += timer_Tick;
            timer.Start();
        }


        void timer_Tick(object sender, EventArgs e)
        {
            //try
            //{
            //    Refresh();
            //}
            //catch (Exception ex)
            //{
            //    ChildWindow errorWin = new ErrorWindow(ex);
            //    errorWin.Show();
            //}
        }

        private void UpdateSelectedItem(object sender, SelectionChangedEventArgs e)
        {

            if (list_ponudbe.SelectedIndex != -1)
            {
                
                vrsta_dela = lbl_vrsta_dela.Content.ToString();
                id_uporabnika = int.Parse(lbl_id_uporabnika.Content.ToString());

                //id_ponudbe = int.Parse(lbl_idponudbe.Content.ToString());
                ////vrsta_orodja = lbl_vrsta_orodja.Content.ToString();
                //cena = lbl_cena.ToString();
                //datum_konca = Convert.ToDateTime(lbl_datum_konca.Content);
                //datum_zacetka = Convert.ToDateTime(lbl_datum_zacetka.Content);
                //opis = lbl_opis.ToString();
                //podjetje = lbl_podjetje.Content.ToString();
                //odgovorna_oseba = lbl_odgovorna_oseba.Content.ToString();
                //tel_st = lbl_tel_st.Content.ToString();
                //email = lbl_email.Content.ToString();


                listBox_priponke.Items.Clear();
                listBox_priponke.SelectedItem = -1;
                if(btn_vrsta_prosta_dela.IsChecked == true)
                    RefreshGrid3(lbl_tip_oglasa.Content.ToString(), int.Parse(lbl_priponke.Content.ToString()));
                else RefreshGrid3(lbl_tip_oglasa.Content.ToString(), 0);
            }
        }

        

        private void DobiPriponke(int idpriponk)
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
                        //priponke_temp.IdPriponke = Convert.ToInt32(reader["idpriponke"]);
                        if (reader["priponka1"] != DBNull.Value)
                        {
                            priponka1 = Convert.ToString(reader["priponka1"]);
                            priponka1_name = Convert.ToString(reader["priponka1_name"]);
                            listBox_priponke.Items.Add(priponka1);
                        }
                        if (reader["priponka2"] != DBNull.Value)
                        {
                            priponka2 = Convert.ToString(reader["priponka2"]);
                            priponka2_name = Convert.ToString(reader["priponka2_name"]);
                            listBox_priponke.Items.Add(priponka2);
                        }
                        if (reader["priponka3"] != DBNull.Value)
                        {
                            priponka3 = Convert.ToString(reader["priponka3"]);
                            priponka3_name = Convert.ToString(reader["priponka3_name"]);
                            listBox_priponke.Items.Add(priponka3);
                        }
                            

                            
                    }
                }
                connection.Close();
            }
        }

        private void download_priponka_Click(object sender, RoutedEventArgs e)
        {
            if (listBox_priponke.SelectedItem != null)
            {
                if(listBox_priponke.SelectedIndex == 0)
                    DownloadPriponka(priponka1, priponka1_name);
                if (listBox_priponke.SelectedIndex == 1)
                    DownloadPriponka(priponka2, priponka2_name);
                if (listBox_priponke.SelectedIndex == 2)
                    DownloadPriponka(priponka3, priponka3_name);
            }
            else MessageBox.Show("Izberi priponko");
        }

        //private void btn_vrsta_vse_Checked(object sender, RoutedEventArgs e)
        //{
        //    Mouse.OverrideCursor = Cursors.Wait;
        //    RefreshGrid3(0, 0);
        //    list_ponudbe.DataContext = new ListaPonudbViewModelPDFilter("", 0);
        //    listBox.Visibility = Visibility.Collapsed;
        //    labelVrstaDela.Visibility = Visibility.Collapsed;
        //    Mouse.OverrideCursor = null;
        //}

        private void btn_vrsta_prosta_dela_Checked(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            RefreshGrid3("", 0);
            list_prevozi.Visibility = Visibility.Hidden;
            list_ponudbe.Visibility = Visibility.Visible;

            list_ponudbe.DataContext = new ListaPonudbViewModelPDFilter("");
            lb_vrste_dela.Visibility = Visibility.Visible;
            lb_vrste_dela.DataContext = new VrsteDelaViewModel();
            lb_vrste_strojev.Visibility = Visibility.Collapsed;
            labelVrstaDela.Visibility = Visibility.Visible;
            Mouse.OverrideCursor = null;
        }

        private void btn_vrsta_proste_kapacitete_Checked(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            RefreshGrid3("", 0);
            list_prevozi.Visibility = Visibility.Hidden;
            list_ponudbe.Visibility = Visibility.Visible;

            list_ponudbe.DataContext = new ListaPonudbViewModelPK("");
            lb_vrste_strojev.Visibility = Visibility.Visible;
            lb_vrste_strojev.DataContext = new PKStrojiViewModel();
            lb_vrste_dela.Visibility = Visibility.Collapsed;
            labelVrstaDela.Visibility = Visibility.Visible;
            Mouse.OverrideCursor = null;
        }

        private void btn_vrsta_prevozi_Checked(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            RefreshGrid3("", 0);
            list_prevozi.DataContext = new PrevoziPrevoznikiViewModel();

            lb_vrste_strojev.Visibility = Visibility.Collapsed;
            lb_vrste_dela.Visibility = Visibility.Collapsed;
            list_ponudbe.Visibility = Visibility.Hidden;
            list_prevozi.Visibility = Visibility.Visible;
            labelVrstaDela.Visibility = Visibility.Hidden;
            Mouse.OverrideCursor = null;
        }

        private void RefreshGrid3(string tip_oglasa, int priponke)
        {
            //opcija 1 - skrij vse
            //opcija 2 - prikazi prosta dela
            //opcija 3 - prikazi proste kapacitete
            //opcija 4 - prikazi prevoze

            //if(tip_oglasa != "NUDIM DELO" && tip_oglasa != "PROSTE KAPACITETE" && tip_oglasa != "PREVOZ")
            if(tip_oglasa == "")
            {
                sp_podrobnosti.Visibility = Visibility.Collapsed;
                sp_sporocilo.Visibility = Visibility.Collapsed;
            }

            if(tip_oglasa == "NUDIM DELO")
            {
                sp_podrobnosti.Visibility = Visibility.Visible;
                sp_sporocilo.Visibility = Visibility.Visible;

                tb_podrobnosti.Visibility = Visibility.Visible;
                tb_vrsta_dela.Visibility = Visibility.Visible;
                tb_dimenzije.Visibility = Visibility.Visible;
                tb_teza.Visibility = Visibility.Visible;
                tb_material.Visibility = Visibility.Visible;
                tb_datum_zacetka.Visibility = Visibility.Visible;
                tb_datum_konca.Visibility = Visibility.Visible;
                tb_cena.Visibility = Visibility.Visible;

                tb_datum_konca_pk.Visibility = Visibility.Collapsed;
                tb_datum_zacetka_pk.Visibility = Visibility.Collapsed;
                tb_vrsta_stroja.Visibility = Visibility.Collapsed;
            }

            if(tip_oglasa == "PROSTE KAPACITETE")
            {
                sp_podrobnosti.Visibility = Visibility.Visible;
                sp_sporocilo.Visibility = Visibility.Visible;

                tb_dimenzije.Visibility = Visibility.Collapsed;
                tb_teza.Visibility = Visibility.Collapsed;
                tb_material.Visibility = Visibility.Collapsed;
                tb_cena.Visibility = Visibility.Collapsed;
                tb_datum_konca.Visibility = Visibility.Collapsed;
                tb_datum_zacetka.Visibility = Visibility.Collapsed;
                tb_vrsta_dela.Visibility = Visibility.Collapsed;

                tb_datum_konca_pk.Visibility = Visibility.Visible;
                tb_datum_zacetka_pk.Visibility = Visibility.Visible;
                tb_vrsta_stroja.Visibility = Visibility.Visible;
            }

            if(tip_oglasa == "PREVOZ")
            {
                //Uredi za prevoze
                sp_podrobnosti.Visibility = Visibility.Collapsed;
                sp_sporocilo.Visibility = Visibility.Collapsed;
            }

            //PRIPONKE
            if (priponke != 0)
            {
                DobiPriponke(priponke);
                listBox_priponke.Visibility = Visibility.Visible;
                download_priponka.Visibility = Visibility.Visible;
                priponke_text.Visibility = Visibility.Visible;
            }

            else
            {
                listBox_priponke.Visibility = Visibility.Collapsed;
                download_priponka.Visibility = Visibility.Collapsed;
                priponke_text.Visibility = Visibility.Collapsed;
            }
        }

        //private void btn_vrsta_vse_Click(object sender, RoutedEventArgs e)
        //{

        //    Mouse.OverrideCursor = Cursors.Wait;

        //    btn_vrsta_vse.IsChecked = true;
        //    btn_vrsta_prosta_dela.IsChecked = false;
        //    btn_vrsta_proste_kapacitete.IsChecked = false;
        //    listBox.Visibility = Visibility.Collapsed;
        //    labelVrstaDela.Visibility = Visibility.Collapsed;
        //    Refresh();

        //}

        //private void btn_vrsta_prosta_dela_Click(object sender, RoutedEventArgs e)
        //{
        //    Mouse.OverrideCursor = Cursors.Wait;

        //    btn_vrsta_prosta_dela.IsChecked = true;
        //    btn_vrsta_proste_kapacitete.IsChecked = false;
        //    btn_vrsta_vse.IsChecked = false;
        //    listBox.Visibility = Visibility.Visible;
        //    listBox.DataContext = new VrsteDelaViewModel(1);
        //    labelVrstaDela.Visibility = Visibility.Visible;
        //    Refresh();
        //}

        //private void btn_vrsta_proste_kapacitete_Click(object sender, RoutedEventArgs e)
        //{

        //    Mouse.OverrideCursor = Cursors.Wait;

        //    btn_vrsta_proste_kapacitete.IsChecked = true;
        //    btn_vrsta_prosta_dela.IsChecked = false;
        //    btn_vrsta_vse.IsChecked = false;
        //    listBox.Visibility = Visibility.Visible;
        //    listBox.DataContext = new VrsteDelaViewModel(2);
        //    labelVrstaDela.Visibility = Visibility.Visible;
        //    Refresh();
        //}

        //private void btn_vrsta_vse_Unchecked(object sender, RoutedEventArgs e)
        //{
        //    btn_vrsta_vse.IsChecked = true;
        //}

        //private void prikazi_priponke()
        //{


        //    if (priponke == 0)
        //    {
        //        tb_priponka1.Visibility = Visibility.Collapsed;
        //        tb_priponka2.Visibility = Visibility.Collapsed;
        //        tb_priponka3.Visibility = Visibility.Collapsed;

        //        btn_priponka1.Visibility = Visibility.Collapsed;
        //        btn_priponka2.Visibility = Visibility.Collapsed;
        //        btn_priponka3.Visibility = Visibility.Collapsed;
        //    }

        //    else
        //    {
        //        if (priponka1 != "")
        //        {
        //            tb_priponka1.Visibility = Visibility.Visible;
        //            btn_priponka1.Visibility = Visibility.Visible;
        //            tb_priponka1.Text = priponka1;
        //        }
        //        else
        //        {
        //            tb_priponka1.Visibility = Visibility.Collapsed;
        //            btn_priponka1.Visibility = Visibility.Collapsed;
        //        }
        //        if (priponka2 != "")
        //        {
        //            tb_priponka2.Visibility = Visibility.Visible;
        //            btn_priponka2.Visibility = Visibility.Visible;
        //            tb_priponka2.Text = priponka2;
        //        }
        //        else
        //        {
        //            tb_priponka2.Visibility = Visibility.Collapsed;
        //            btn_priponka2.Visibility = Visibility.Collapsed;
        //        }
        //        if (priponka3 != "")
        //        {
        //            tb_priponka3.Visibility = Visibility.Visible;
        //            btn_priponka3.Visibility = Visibility.Visible;
        //            tb_priponka3.Text = priponka3;
        //        }
        //        else
        //        {
        //            tb_priponka3.Visibility = Visibility.Collapsed;
        //            btn_priponka3.Visibility = Visibility.Collapsed;
        //        }
        //    }
        //}

        //private void btn_priponka1_click(object sender, RoutedEventArgs e)
        //{
        //    DownloadPriponka(priponka1_name);
        //}

        //private void btn_priponka2_click(object sender, RoutedEventArgs e)
        //{
        //    DownloadPriponka(tb_priponka2.Text);
        //}

        //private void btn_priponka3_click(object sender, RoutedEventArgs e)
        //{
        //    DownloadPriponka(tb_priponka3.Text);
        //}

        private void DownloadPriponka(string filename, string actual_name)
        {
            string server = "192.168.1.125:21";
            string inputfilepath = @"C:\" + filename;
            //string ftphost = "xxx.xx.x.xxx";
            //string ftpfilepath = "/Updater/Dir1/FileName.exe";

            string ftpfullpath = "ftp://" + server + "/ftp/" + actual_name;

            //Stream myStream;
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "All files (*.*)|*.*";
            sfd.FileName = filename;
            //sfd.FilterIndex = 2;
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
                        }
                        MessageBox.Show("Download Complete");
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

            
        }

        //private void Refresh()
        //{
        //    if (btn_vrsta_vse.IsChecked == true)
        //        list_ponudbe.DataContext = new ListaPonudbViewModelPDFilter("", 0);
        //    else if (btn_vrsta_prosta_dela.IsChecked == true)
        //        list_ponudbe.DataContext = new ListaPonudbViewModelPDFilter("", 1);
        //    else if (btn_vrsta_proste_kapacitete.IsChecked == true)
        //        list_ponudbe.DataContext = new ListaPonudbViewModelPDFilter("", 2);

            

        //    id_ponudbe = 0;
        //    vrsta_dela = "";
        //    //vrsta_orodja = "";
        //    cena = "";
        //    datum_konca = DateTime.Now;
        //    datum_zacetka = DateTime.Now;
        //    opis = "";
        //    podjetje = "";
        //    odgovorna_oseba = "";
        //    tel_st = "";
        //    email = "";
        //    id_uporabnika = 0;

        //    Mouse.OverrideCursor = null;

        //    //btn_izbrisi.IsEnabled = false;
        //    //btn_uredi.IsEnabled = false;
        //    //btn_povprasevanje.IsEnabled = false;
        //}F
    }
}
