using System;
using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using SmartLibrary.Avalonia.ViewModels;
using SmartLibrary.Common.Interfaces;
using SmartLibrary.Common.Messages;
using SmartLibrary.Common.ViewModels;

namespace SmartLibrary.Avalonia.Services;
public class AvaloniaNavigationService : INavigationService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IDialogService _dialogService;
    private readonly IMessenger _messenger;

    public AvaloniaNavigationService(IServiceProvider serviceProvider, IDialogService dialogService, IMessenger messenger)
    {
        _serviceProvider = serviceProvider;
        _dialogService = dialogService;
        _messenger = messenger;
    }

    public Func<string, string> GetViewName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public Task NavigateAsync<T>(params (string key, object value)[] data) where T : BaseViewModel
    {
        var shell = ((Application.Current!.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)!.MainWindow!).DataContext as ShellViewModel;
        var target = _serviceProvider.GetService(typeof(T)) as BaseViewModel;
        var parameters = data.ToDictionary(kv => kv.key, kv => kv.value);
        shell!.CurrentModule = target;
        target!.OnNavigatedTo(parameters);
        _messenger.Send(new NavigationMessage(target, false));
        shell.ShowItemCommand.NotifyCanExecuteChanged();
        return Task.CompletedTask;
    }

    public async Task<(bool result, T dialog)> ShowDialogAsync<T>(params (string key, object value)[] data) where T : BaseViewModel
    {
        var parameters = data.ToDictionary(kv => kv.key, kv => kv.value);
        var target = _serviceProvider.GetService<T>()!;
        _messenger.Send(new NavigationMessage(target, false));
        var result = await _dialogService.ShowDialogAsync<T>(target!, parameters);
        return result;
    }
}
