namespace SmartLibrary.Common.Interfaces;

public interface IThemeService
{
    void ChangeTheme(string themeName);

    IEnumerable<KeyValuePair<string, string>> GetThemeNames();
}

