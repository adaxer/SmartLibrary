using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SmartLibrary.Common.Interfaces;
using SmartLibrary.Common.ViewModels;
using SmartLibrary.Common.Extensions;
using SmartLibrary.Core.Localization;
using CommunityToolkit.Mvvm.Messaging;
using SmartLibrary.Common.Messages;
using SmartLibrary.Avalonia.Interfaces;
using System;

namespace SmartLibrary.Avalonia.ViewModels;
public partial class ShellViewModel : BaseViewModel, IShellViewModel, IRecipient<NavigationMessage>, IRecipient<StatusMessage>
{
    private readonly INavigationService _navigationService;
    private readonly ILocalizationService _localizationService;

    public ShellViewModel(WelcomeViewModel welcome, INavigationService navigationService, ILocalizationService localizationService, IPubSubService pubSub)
    {
        CurrentModule = welcome;
        _navigationService = navigationService;
        _localizationService = localizationService;
        Title = _localizationService.Get("AppTitle");
        InitializeNavigation();
        pubSub.Subscribe<NavigationMessage>(this);
        pubSub.Subscribe<StatusMessage>(this);
    }

    [ObservableProperty]
    BaseViewModel? _currentModule;

    [ObservableProperty]
    bool _isExpanded = !(OperatingSystem.IsIOS() || OperatingSystem.IsAndroid());

    [ObservableProperty]
    string _statusText = string.Empty;

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
        IsBusy = true;
        await Task.Delay(2000);
        IsBusy = false;
        return "Internet is available";
    }

    public bool CanShowItem(MenuEntry entry) => entry.IsModal || !entry.TargetType.Equals(CurrentModule?.GetType());

    private void InitializeNavigation()
    {
        Modules.Add(new MenuEntry(_localizationService.Get("Welcome"), typeof(WelcomeViewModel), "human-greeting-variant"));
        Modules.Add(new MenuEntry(_localizationService.Get("Search"), typeof(SearchViewModel), "magnify"));
        Modules.Add(new MenuEntry(_localizationService.Get("Settings"), typeof(SettingsViewModel), "cog-outline"));
        Dialogs.Add(new MenuEntry(_localizationService.Get("About"), typeof(AboutViewModel), "information-outline", true));
        Dialogs.Add(new MenuEntry(_localizationService.Get("LoginOrRegister"), typeof(LoginViewModel), "login", true));
    }

    public void Receive(NavigationMessage message) => Title = $"""{_localizationService.Get("AppTitle")} - {message.Target.Title}""";

    public void Receive(StatusMessage message) => StatusText = message.Value;
}
