using System;
using System.Globalization;
using Xamarin.Forms;

namespace MijnSauna.Frontend.Phone.Converters
{
    public class TypeNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null ? value.GetType().Name : string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Unsupported!");
        }
    }
}