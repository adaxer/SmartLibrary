using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace SmartLibrary.Wpf.Converters;
public class InverseBoolToVisibilityConverter : IValueConverter
{
    public bool CollapseInsteadOfHide { get; set; } = true;

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool booleanValue)
        {
            return booleanValue ?
                (CollapseInsteadOfHide ? Visibility.Collapsed : Visibility.Hidden) :
                Visibility.Visible;
        }
        return Visibility.Visible;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Visibility visibility)
        {
            return visibility == Visibility.Visible ? false : true;
        }
        return false;
    }
}
