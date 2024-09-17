using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using SmartLibrary.Common.Interfaces;
using SmartLibrary.Common.ViewModels;

namespace SmartLibrary.Avalonia.ViewModels;

public partial class SettingsViewModel : BaseViewModel
{
    private readonly IThemeService _themeService;

    public SettingsViewModel(IThemeService themeService)
    {
        _themeService = themeService;
        Themes = themeService.GetThemeNames().ToList();
        CurrentTheme = Themes.First();
    }

    [ObservableProperty]
    List<KeyValuePair<string, string>> _themes = default!;

    [ObservableProperty]
    KeyValuePair<string, string> _currentTheme = default!;

    partial void OnCurrentThemeChanged(KeyValuePair<string, string> oldValue, KeyValuePair<string, string> newValue)
    {
        if (!string.IsNullOrEmpty(newValue.Key))
        {
            _themeService.ChangeTheme(newValue.Key);
        }
    }
}
