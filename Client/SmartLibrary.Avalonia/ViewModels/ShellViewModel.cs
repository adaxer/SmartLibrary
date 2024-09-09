using System;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using SmartLibrary.Common.ViewModels;

namespace SmartLibrary.Avalonia.ViewModels;
public partial class ShellViewModel : BaseViewModel
{
    public ShellViewModel(IServiceProvider serviceProvider)
    {
        CurrentModule = serviceProvider.GetService<MainViewModel>();
    }

    [ObservableProperty]
    BaseViewModel? _currentModule;
}
