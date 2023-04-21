//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Data;

//namespace PaDesktop.Converter
//{
//    public class NullableConverter<S, T> : IValueConverter where T : S?
//    {
//        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
//        {
//            return Convert((S)value);
//        }

//        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
//        {
//            return ConvertBack2((T)value);
//        }

//        private T Convert(S value)
//        {
//            return (T)value;
//        }

//        private S ConvertBack2(T value)
//        {
//            if (value is null) return default(S);
//            return (S)value;
//        }

//    }
//}
