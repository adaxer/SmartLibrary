using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace SmartLibrary.Wpf.Converters;
public class StringToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string str && !string.IsNullOrWhiteSpace(str))
        {
            return Visibility.Visible;
        }

        return Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException("StringToVisibilityConverter can only be used for one-way conversion.");
    }
}
