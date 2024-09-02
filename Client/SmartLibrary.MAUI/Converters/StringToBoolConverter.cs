
namespace SmartLibrary.MAUI.Converters;
public class StringToBoolConverter : IValueConverter
{
    public int MinLength { get; set; } = 0;

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        // Super-interessant: in C# Pattern Matching einlesen!
        return value switch
        {
            null => false,
            "" => false,
            string s => s.Length >= MinLength,
            _ => false
        };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value == null
            ? "Undefined"
            : value.ToString();
    }
}
