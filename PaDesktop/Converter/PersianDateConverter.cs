using Mohsen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PaDesktop.Converter
{

    /// <summary>
    /// Converts DateTime values to PersianDate values and vice versa, to be used in XAML
    /// </summary>
    [ValueConversion(typeof(DateTime), typeof(PersianDate))]
    public class DateTimeToPersianDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            DateTime date = (DateTime?)value ?? DateTime.Now;
            return new PersianDate(date);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return DateTime.Now;
            PersianDate pDate = (PersianDate)value;
            return pDate.ToDateTime();
        }
    }

}
