using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Orodjarne
{
    public class ByteArrayToImageConverter : IValueConverter
    {
        //public BitmapImage ToImage(byte[] array)
        //{
        //    using (var ms = new System.IO.MemoryStream(array))
        //    {
        //        var image = new BitmapImage();
        //        image.BeginInit();
        //        image.CacheOption = BitmapCacheOption.OnLoad;
        //        image.StreamSource = ms;
        //        image.EndInit();
        //        return image;
        //    }
        //}

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null && value is byte[])
            {
                byte[] byteBlob = value as byte[];
                //byte[] byteBlob = new byte[16 * 1024];
                MemoryStream ms = new MemoryStream(byteBlob);
                BitmapImage bmi = new BitmapImage();

                bmi.BeginInit();
                bmi.StreamSource = ms;
                bmi.EndInit();
                //bmi.Source(ms);
                //bmi.UriSource = new Uri(ms.ToString());

                return bmi;
            }
            return null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        //public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        //{
        //    try
        //    {
        //        if (value != null)
        //        {
        //            System.Data.Linq.Binary bnr = (System.Data.Linq.Binary)value;
        //            byte[] b = bnr.ToArray();
        //            MemoryStream stream = new MemoryStream(b);

        //            BitmapImage image = new BitmapImage();
        //            image.BeginInit();
        //            image.StreamSource = stream;
        //            image.EndInit();
        //            return image;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Podrobnosti:" + System.Environment.NewLine + System.Environment.NewLine + ex.Message, "Težave s slikami v podatkovni bazi.", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }

        //    return null;
        //}

        //public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        //{
        //    System.Diagnostics.Debugger.Break();
        //    throw new Exception("The method or operation is not implemented.");
        //}
    }
}
