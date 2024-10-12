using System;
using System.Globalization;
using System.Windows.Data;

namespace WeatherApp.ViewModel.ValueConverters
{
    public class BoolRainConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool and true)
            {
                return "Raining";
            }

            return "There is no rain";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.Equals(value as string, "Raining"))
            {
                return true;
            }

            return false;
        }
    }
}