using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using SmartLibrary.Common.Interfaces;
using SmartLibrary.Common.ViewModels;
using SmartLibrary.Common.Extensions;
using SmartLibrary.Core.Localization;

namespace SmartLibrary.Avalonia.ViewModels;
public partial class ShellViewModel : BaseViewModel
{
    public ShellViewModel(IServiceProvider serviceProvider, INavigationService navigationService, ILocalizationService localizationService)
    {
        CurrentModule = serviceProvider.GetService<MainViewModel>();
        InitializeModules();
        _navigationService = navigationService;
        Title = localizationService.Get("AppTitle");
    }

    [ObservableProperty]
    BaseViewModel? _currentModule;

    [ObservableProperty]
    ObservableCollection<KeyValuePair<string, Type>> _modules = new();
    private readonly INavigationService _navigationService;

    [RelayCommand(CanExecute = nameof(CanShowModule))]
    private async Task ShowModuleAsync(Type type)
    {
        await _navigationService.NavigateAsync(type);
    }

    public Task<string> InternetStatus => GetInternetStatusAsync();

    private async Task<string> GetInternetStatusAsync()
    {
        await Task.Delay(3000);
        return "Internet is available";
    }

    public bool CanShowModule(Type type) => !type.Equals(CurrentModule?.GetType());

    private void InitializeModules()
    {
        Modules.Add(new KeyValuePair<string, Type>("Welcome", typeof(MainViewModel)));
        Modules.Add(new KeyValuePair<string, Type>("About", typeof(AboutViewModel)));
        Modules.Add(new KeyValuePair<string, Type>("Search", typeof(SearchViewModel)));
    }
}
