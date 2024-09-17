using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Styling;
using SmartLibrary.Common.Interfaces;
using SmartLibrary.Core.Localization;

namespace SmartLibrary.Avalonia.Services;
public class AvaloniaThemeService : IThemeService
{
    private static List<(string Key, string Value, ThemeVariant Theme)> _themes = new()
    {
        (nameof(Strings.ThemeDefault),Strings.ThemeDefault,ThemeVariant.Default),
        (nameof(Strings.ThemeDark),Strings.ThemeDark,ThemeVariant.Dark),
        (nameof(Strings.ThemeLight),Strings.ThemeLight,ThemeVariant.Light),
    };

    public void ChangeTheme(string themeName)
    {
        if (_themes.FirstOrDefault(t => t.Key == themeName) is { } tuple)
        {
            Application.Current!.RequestedThemeVariant = tuple.Theme;
        }
    }

    public IEnumerable<KeyValuePair<string, string>> GetThemeNames() =>
        _themes.Select(x => new KeyValuePair<string, string>(x.Key, x.Value));
}
