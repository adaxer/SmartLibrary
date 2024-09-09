using System;
using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Microsoft.Extensions.DependencyInjection;
using SmartLibrary.Avalonia.ViewModels;
using SmartLibrary.Common.Interfaces;
using SmartLibrary.Common.ViewModels;

namespace SmartLibrary.Avalonia.Services;
public class AvaloniaNavigationService : INavigationService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IDialogService _dialogService;

    public AvaloniaNavigationService(IServiceProvider serviceProvider, IDialogService dialogService)
    {
        _serviceProvider = serviceProvider;
        _dialogService = dialogService;
    }

    public Func<string, string> GetViewName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public Task NavigateAsync<T>(params (string key, object value)[] data) where T : BaseViewModel
    {
        var shell = ((Application.Current!.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)!.MainWindow!).DataContext as ShellViewModel;
        var target = _serviceProvider.GetService(typeof(T)) as BaseViewModel;
        var parameters = data.ToDictionary(kv => kv.key, kv => kv.value);
        shell!.CurrentModule = target;
        target!.OnNavigatedTo(parameters);
        return Task.CompletedTask;
    }

    public async Task<(bool result, T dialog)> ShowDialogAsync<T>(params (string key, object value)[] data) where T : BaseViewModel
    {
        var parameters = data.ToDictionary(kv => kv.key, kv => kv.value);
        var target = _serviceProvider.GetService<T>();
        var result = await _dialogService.ShowDialogAsync<T>(target!, parameters);
        return result;
    }
}
