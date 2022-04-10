﻿using MijnSauna.Common.DataTransferObjects.Sessions;
using System.Globalization;

namespace MijnSauna.Frontend.Maui.Converters
{
    public class ActiveSessionToLogoConverter : IValueConverter
    {
        public string Idle { get; set; }
        public string Sauna { get; set; }
        public string Infrared { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is GetActiveSessionResponse activeSession)
            {
                if (activeSession.IsSauna)
                {
                    return Sauna;
                }

                if (activeSession.IsInfrared)
                {
                    return Infrared;
                }
            }

            return Idle;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Unsupported!");
        }
    }
}