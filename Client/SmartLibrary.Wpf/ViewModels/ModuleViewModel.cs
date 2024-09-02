using System;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SmartLibrary.Common.Interfaces;
using SmartLibrary.Common.ViewModels;
using SmartLibrary.Core.Localization;

namespace SmartLibrary.Wpf;
public partial class ModuleViewModel<T> : BaseViewModel, IModuleViewModel where T : BaseViewModel
{
    private readonly INavigationService _navigationService;
    private readonly IServiceProvider _serviceProvider;

    public ModuleViewModel(INavigationService navigationService, IServiceProvider serviceProvider, ILocalizationService localizationService)
    {
        _navigationService = navigationService;
        _serviceProvider = serviceProvider;
        NavigateCommand = new RelayCommand(async () => await NavigateAsync());
        Title = localizationService.Get(TargetType.Name.Replace("ViewModel",""));
    }

    [ObservableProperty]
    Type _targetType = typeof(T);

    [ObservableProperty]
    bool _isTopLevel= true;

    [ObservableProperty]
    ICommand _navigateCommand;
    private async Task NavigateAsync()
    {
        await _navigationService.NavigateAsync<T>();
        IsTopLevel = true;

    }
}

public interface IModuleViewModel
{
    string Title { get; }

    ICommand NavigateCommand { get; }

    bool IsTopLevel { get; set; }

    Type TargetType { get; }
}
