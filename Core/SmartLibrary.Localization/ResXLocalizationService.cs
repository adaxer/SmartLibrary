using System.Diagnostics;
using System.Globalization;
using System.Reflection;

namespace SmartLibrary.Core.Localization;

public class ResXLocalizationService : ILocalizationService
{
    public CultureInfo CurrentCulture
    {
        get => Strings.Culture;
        set => Strings.Culture = value;
    }

    public string Get(string key, string defaultValue)
    {
        try
        {
            object result = typeof(Strings)
                .GetProperty(key, BindingFlags.Public | BindingFlags.Static | BindingFlags.GetProperty)?
                .GetValue(null)!;
            return (result is string value)
                ? value
                : defaultValue;
        }
        catch (Exception ex)
        {
            Trace.TraceError($"{ex}");
            return defaultValue;
        }
    }


}
