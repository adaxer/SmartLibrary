using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SmartLibrary.Common.Interfaces;
using SmartLibrary.Common.ViewModels;
using SmartLibrary.Common.Extensions;
using SmartLibrary.Core.Localization;

namespace SmartLibrary.Avalonia.ViewModels;
public partial class ShellViewModel : BaseViewModel
{
    private readonly INavigationService _navigationService;
    private readonly ILocalizationService _localizationService;

    public ShellViewModel(MainViewModel main, INavigationService navigationService, ILocalizationService localizationService)
    {
        CurrentModule = main;
        _navigationService = navigationService;
        _localizationService = localizationService;
        Title = _localizationService.Get("AppTitle");
        InitializeNavigation();
    }

    [ObservableProperty]
    BaseViewModel? _currentModule;

    [ObservableProperty]
    bool _isExpanded = true;

    [ObservableProperty]
    ObservableCollection<MenuEntry> _modules = new();

    [ObservableProperty]
    ObservableCollection<MenuEntry> _dialogs = new();

    [RelayCommand]
    private void ToggleExpanded() => IsExpanded ^= true;

    [RelayCommand(CanExecute = nameof(CanShowItem))]
    private async Task ShowItemAsync(MenuEntry entry)
    {
        IsExpanded = false;
        if (entry.IsModal)
        {
            await _navigationService.ShowDialogAsync(entry.TargetType);
        }
        else
        {
            await _navigationService.NavigateAsync(entry.TargetType);
        }
    }

    public Task<string> InternetStatus => GetInternetStatusAsync();

    private async Task<string> GetInternetStatusAsync()
    {
        await Task.Delay(3000);
        return "Internet is available";
    }

    public bool CanShowItem(MenuEntry entry) => entry.IsModal || !entry.TargetType.Equals(CurrentModule?.GetType());

    private void InitializeNavigation()
    {
        Modules.Add(new MenuEntry(_localizationService.Get("Main"), typeof(MainViewModel), "human-greeting-variant"));
        Modules.Add(new MenuEntry(_localizationService.Get("Search"), typeof(SearchViewModel), "magnify"));
        Modules.Add(new MenuEntry(_localizationService.Get("Settings"), typeof(SettingsViewModel), "cog-outline"));
        Dialogs.Add(new MenuEntry(_localizationService.Get("About"), typeof(AboutViewModel), "information-outline", true));
        Dialogs.Add(new MenuEntry(_localizationService.Get("LoginOrRegister"), typeof(LoginViewModel), "login", true));
    }
}
