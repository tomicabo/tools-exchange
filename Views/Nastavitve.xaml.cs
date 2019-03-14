using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace Orodjarne.Views
{
    /// <summary>
    /// Interaction logic for Nastavitve.xaml
    /// </summary>
    public partial class Nastavitve : UserControl
    {

        byte[] slika;

        public Nastavitve()
        {
            InitializeComponent();

            Refresh();

        }

        private void btn_shrani_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["myDatabaseConnection"].ConnectionString;

                MessageBoxResult result = MessageBox.Show("Ali želite urediti nastavitve?", "Uredi podatke", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)

                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();

                        var query = "update uporabniki set podjetje = @Podjetje, odgovorna_oseba = @OdgovornaOseba, uporabnisko_ime = @UporabniskoIme, tel_st = @TelSt, email= @Email, logo = @Logo where id = " + (Application.Current as App).uporabnikId + ";";

                        MySqlCommand command = new MySqlCommand(query, connection);
                        if (tb_podjetje.Text != null)
                            command.Parameters.AddWithValue("@Podjetje", tb_podjetje.Text);
                        if (tb_odgovorna_oseba.Text != null)
                            command.Parameters.AddWithValue("@OdgovornaOseba", tb_odgovorna_oseba.Text);
                        if (tb_uporabnisko_ime.Text != null)
                            command.Parameters.AddWithValue("@UporabniskoIme", tb_uporabnisko_ime.Text);
                        //command.Parameters.AddWithValue("@Geslo", geslo);
                        if (tb_telefonska_st.Text != null)
                            command.Parameters.AddWithValue("@TelSt", tb_telefonska_st.Text);
                        if (tb_email.Text != null)
                            command.Parameters.AddWithValue("@Email", tb_email.Text);
                        if (slika != null)
                            command.Parameters.AddWithValue("@Logo", slika);
                        else command.Parameters.AddWithValue("@Logo", ((Application.Current as App).logo));


                        command.ExecuteNonQuery();

                        connection.Close();

                        if (slika != null)
                            (Application.Current as App).logo = slika;
                    }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }


        private void Refresh()
        {

            tb_podjetje.Text = (Application.Current as App).podjetje;
            tb_odgovorna_oseba.Text = (Application.Current as App).odgovorna_oseba;
            tb_uporabnisko_ime.Text = (Application.Current as App).uporabnisko_ime;
            tb_telefonska_st.Text = (Application.Current as App).tel_st.ToString();
            tb_email.Text = (Application.Current as App).email;

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

        }

        private void btn_dodaj_sliko_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog file_dialog = new OpenFileDialog();

            file_dialog.Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|GIF Files (*.gif)|*.gif|PNG Files (*.png)|*.png|All files (*.*)|*.*";
            file_dialog.Title = "Izberi sliko";
            //bool? izbran = file_dialog.ShowDialog();

            

            

            try
            {
                if (file_dialog.ShowDialog() == true)
                {
                    FileInfo fi = new FileInfo(file_dialog.FileName);

                    if (fi.Length < 1000000)
                    {
                        //BitmapImage bmi = new BitmapImage();
                        //bmi.SetSource(file_dialog.File.OpenRead());
                        //slika = ReadBytesFromStream(file_dialog.File.OpenRead());
                        //image_box.Source = bmi;
                        slika = ReadBytesFromStream(file_dialog.OpenFile());
                        image_box.Source = new BitmapImage(new Uri(file_dialog.FileName));
                    }
                    else MessageBox.Show("Prevelika slika");
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static byte[] ReadBytesFromStream(Stream stream)
        {
            byte[] size = new byte[16 * 1024];
            using (MemoryStream memoryStream = new MemoryStream())
            {
                int readCount;
                while ((readCount = stream.Read(size, 0, size.Length)) > 0)
                {
                    memoryStream.Write(size, 0, readCount);
                }
                return memoryStream.ToArray();
            }
        }
    }
}
