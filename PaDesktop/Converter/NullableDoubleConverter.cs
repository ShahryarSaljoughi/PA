using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PaDesktop.Converter
{

    [ValueConversion(typeof(double?), typeof(double))]
    public class NullableDoubleConverter : IValueConverter
    {
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var source = (double?)value;
            if (source.HasValue) { return source.Value; }
            return default(double);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double?)value;
        }
    }

    [ValueConversion(typeof(int), typeof(int?))]
    public class NullableIntConverter : IValueConverter
    {
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int result = default;
            int? source;
            try
            {
                source = (int?)(double)value;
            }
            catch (Exception)
            {
                source = null;
            }
            if (source.HasValue) { result = source.Value; }
            return result; 
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int?)value;
        }
    }
}
