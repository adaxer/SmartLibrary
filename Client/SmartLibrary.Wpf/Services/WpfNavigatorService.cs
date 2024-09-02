using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using SmartLibrary.Common.Interfaces;
using SmartLibrary.Common.Messages;
using SmartLibrary.Common.ViewModels;
using SmartLibrary.Wpf.Interfaces;

namespace SmartLibrary.Wpf.Services;

public class WpfNavigatorService : INavigationService
{
    Dictionary<string, Dictionary<string, object>> currentParameters = new();
    private readonly IServiceProvider _serviceProvider;
    private readonly IDialogService _dialogService;
    private readonly IMessenger _messenger;

    public Func<string, string> GetViewName { get; set; }

    public WpfNavigatorService(IServiceProvider serviceProvider, IDialogService dialogService, IMessenger messenger)
    {
        _serviceProvider = serviceProvider;
        _dialogService = dialogService;
        _messenger = messenger;
        GetViewName = GetViewNameDefault;
    }

    public Task NavigateAsync<T>(params (string key, object value)[] data) where T : BaseViewModel
    {
        var shell = Application.Current.MainWindow.DataContext as ShellViewModel;
        var target = _serviceProvider.GetService(typeof(T)) as BaseViewModel;
        var parameters = data.ToDictionary(kv => kv.key, kv => kv.value);
        shell.CurrentContent = target;
        target.OnNavigatedTo(parameters);
        _messenger.Send(new NavigationMessage(target, false));
        return Task.CompletedTask;
    }

    public async Task<(bool result, T dialog)> ShowDialogAsync<T>(params (string key, object value)[] data) where T : BaseViewModel
    {
        var parameters = data.ToDictionary(kv => kv.key, kv => kv.value);
        var target = _serviceProvider.GetService<T>();
        _messenger.Send(new NavigationMessage(target, true));
        var result = await _dialogService.ShowDialogAsync<T>(target, parameters);
        return result;
    }

    private string GetViewNameDefault(string targetName)
    {
        return targetName.Replace("ViewModel", "Page");
    }
}
