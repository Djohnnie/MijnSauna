﻿using MijnSauna.Frontend.Maui.Enums;
using System.Globalization;

namespace MijnSauna.Frontend.Maui.Converters
{
    public class SessionStateToColorConverter : IValueConverter
    {
        public Color NoneColor { get; set; }
        public Color ActiveColor { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SessionState sessionState)
            {
                if (sessionState == SessionState.None)
                {
                    return NoneColor;
                }

                if (sessionState == SessionState.Active)
                {
                    return ActiveColor;
                }
            }

            return NoneColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Unsupported!");
        }
    }
}