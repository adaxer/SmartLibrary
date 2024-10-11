using System;
using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Microsoft.Extensions.DependencyInjection;
using SmartLibrary.Avalonia.ViewModels;
using SmartLibrary.Common.Interfaces;
using SmartLibrary.Common.Messages;
using SmartLibrary.Common.ViewModels;

namespace SmartLibrary.Avalonia.Android.Platform;
public class AndroidNavigationService : INavigationService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IDialogService _dialogService;
    private readonly IPubSubService _pubsub;

    public AndroidNavigationService(IServiceProvider serviceProvider, IDialogService dialogService, IPubSubService pubsub)
    {
        _serviceProvider = serviceProvider;
        _dialogService = dialogService;
        _pubsub = pubsub;
    }

    public Func<string, string> GetViewName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public Task NavigateAsync<T>(params (string key, object value)[] data) where T : BaseViewModel
    {
        var shell = (Application.Current!.ApplicationLifetime as ISingleViewApplicationLifetime)!.MainView!.DataContext as ShellViewModel;
        var target = _serviceProvider.GetService(typeof(T)) as BaseViewModel;
        var parameters = data.ToDictionary(kv => kv.key, kv => kv.value);
        shell!.CurrentModule = target;
        target!.OnNavigatedTo(parameters);
        _pubsub.Publish(new NavigationMessage(target, false));
        shell.ShowItemCommand.NotifyCanExecuteChanged();
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
}
