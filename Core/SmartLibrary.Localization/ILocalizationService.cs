using System.Globalization;

namespace SmartLibrary.Core.Localization;
public interface ILocalizationService
{
    string Get(string key, string defaultValue="?loc?");
    CultureInfo CurrentCulture { get; set; }
}
