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
    /// Interaction logic for MojiOglasi.xaml
    /// </summary>
    public partial class MojiOglasi : UserControl
    {
        public MojiOglasi()
        {
            InitializeComponent();
            rb_prosta_dela.IsChecked = true;
        }

        private void rb_prosta_dela_Checked(object sender, RoutedEventArgs e)
        {
            list_ponudbe.DataContext = new MojaListaPonudbViewModel();
            list_ponudbe.Visibility = Visibility.Visible;
            list_prevozi.Visibility = Visibility.Hidden;
        }

        private void rb_proste_kapacitete_Checked(object sender, RoutedEventArgs e)
        {
            list_ponudbe.DataContext = new MojaListaPonudbViewModelPK();
            list_ponudbe.Visibility = Visibility.Visible;
            list_prevozi.Visibility = Visibility.Hidden;
        }

        private void rb_prevozi_Checked(object sender, RoutedEventArgs e)
        {
            list_prevozi.DataContext = new MojiPrevoziIskalciViewModel();
            list_ponudbe.Visibility = Visibility.Hidden;
            list_prevozi.Visibility = Visibility.Visible;
        }

        private void UpdateSelectedItem(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btn_dodaj_oglas_Click(object sender, RoutedEventArgs e)
        {
            if (rb_prosta_dela.IsChecked == true)
            {
                PDInsert okno = new PDInsert();

                okno.ShowDialog();
                if(okno.DialogResult == true)
                    list_ponudbe.DataContext = new MojaListaPonudbViewModel();

            }
            else if(rb_proste_kapacitete.IsChecked == true)
            {
                PKInsert okno = new PKInsert();
                okno.ShowDialog();
                if(okno.DialogResult == true)
                    list_ponudbe.DataContext = new MojaListaPonudbViewModelPK();
            }
            else if (rb_prevozi.IsChecked == true)
            {
                PRInsertIskalec okno = new PRInsertIskalec();
                okno.Show();
            }
        }

        
    }
}
