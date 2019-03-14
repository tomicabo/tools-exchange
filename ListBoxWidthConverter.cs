using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Orodjarne
{
    class ListBoxWidthConverter : IValueConverter
    {
        //private double _actualWidth;
        //public double ActualWidth
        //{
        //    get
        //    {
        //        return _actualWidth;
        //    }
        //    set
        //    {
        //        if (_actualWidth != value)
        //        {
        //            _actualWidth = value;
        //            OnPropertyChanged("ActualWidth");
        //            OnPropertyChanged("ChangedWidth");
        //        }
        //    }
        //}

        //private void OnPropertyChanged(string v)
        //{
        //    throw new NotImplementedException();
        //}

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)

        {
            return double.Parse(value.ToString()) - 50;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
