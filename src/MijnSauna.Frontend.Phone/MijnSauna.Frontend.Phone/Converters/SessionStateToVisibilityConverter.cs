using System;
using System.Globalization;
using MijnSauna.Frontend.Phone.Enums;
using Xamarin.Forms;

namespace MijnSauna.Frontend.Phone.Converters
{
    public class SessionStateToVisibilityConverter : IValueConverter
    {
        public bool NoneVisibility { get; set; }
        public bool ActiveVisibility { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SessionState sessionState)
            {
                if (sessionState == SessionState.None)
                {
                    return NoneVisibility;
                }

                if (sessionState == SessionState.Active)
                {
                    return ActiveVisibility;
                }
            }

            return NoneVisibility;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Unsupported!");
        }
    }
}