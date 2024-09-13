using Avalonia.Data.Converters;
using Avalonia.Media;
using System;

namespace CustomDrawDemo;
public class ColorStringToBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (value is string colorString && Color.TryParse(colorString, out Color color))
        {
            return new SolidColorBrush(color);
        }
        return Brushes.Transparent;
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        return value;
    }
}