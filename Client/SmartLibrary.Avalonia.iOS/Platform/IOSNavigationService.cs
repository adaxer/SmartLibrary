using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls.ApplicationLifetimes;
using Microsoft.Extensions.DependencyInjection;
using SmartLibrary.Avalonia.ViewModels;
using SmartLibrary.Common.Interfaces;
using SmartLibrary.Common.Messages;
using SmartLibrary.Common.ViewModels;

namespace SmartLibrary.Avalonia.iOS.Platform;
public class IOSNavigationService : INavigationService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IDialogService _dialogService;
    private readonly IPubSubService _pubsub;
    private Stack<BaseViewModel> _backStack = new();
    private ShellViewModel _shell = default!;

    public IOSNavigationService(IServiceProvider serviceProvider, IDialogService dialogService, IPubSubService pubsub)
    {
        _serviceProvider = serviceProvider;
        _dialogService = dialogService;
        _pubsub = pubsub;
    }

    public Func<string, string> GetViewName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public Task NavigateAsync<T>(params (string key, object value)[] data) where T : BaseViewModel
    {
        _shell = ((global::Avalonia.Application.Current!.ApplicationLifetime as ISingleViewApplicationLifetime)!.MainView!.DataContext as ShellViewModel)!;
        var target = _serviceProvider.GetService(typeof(T)) as BaseViewModel;
        if (target is INavigateBack && _shell.CurrentModule is BaseViewModel lastTarget)
        {
            _backStack.Push(lastTarget);
        }
        else
        {
            _backStack.Clear();
        }
        var parameters = data.ToDictionary(kv => kv.key, kv => kv.value);
        _shell.CurrentModule = target;
        target!.OnNavigatedTo(parameters);
        _pubsub.Publish(new NavigationMessage(target, false));
        _shell.ShowItemCommand.NotifyCanExecuteChanged();
        return Task.CompletedTask;
    }

    public async Task<(bool result, T dialog)> ShowDialogAsync<T>(params (string key, object value)[] data) where T : BaseViewModel
    {
        var parameters = data.ToDictionary(kv => kv.key, kv => kv.value);
        var target = _serviceProvider.GetService<T>()!;
        _pubsub.Publish(new NavigationMessage(target, false));
        var result = await _dialogService.ShowDialogAsync(target!, parameters);
        return result;
    }

    bool INavigationService.CanGoBack()
    {
        return _backStack.Any();
    }

    Task INavigationService.GoBack()
    {
        var lastTarget = _backStack.Pop();
        _shell.CurrentModule = lastTarget;
        _pubsub.Publish(new NavigationMessage(lastTarget, false));
        _shell.ShowItemCommand.NotifyCanExecuteChanged();
        return Task.CompletedTask;
    }
}
