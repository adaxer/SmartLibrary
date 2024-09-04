using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SmartLibrary.Common.Interfaces;
using SmartLibrary.Common.ViewModels;

namespace SmartLibrary.Avalonia.Services;
public class AvaloniaNavigationService : INavigationService
{
    private readonly IServiceProvider _serviceProvider;

    public AvaloniaNavigationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Func<string, string> GetViewName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public Task NavigateAsync<T>(params (string key, object value)[] data) where T : BaseViewModel
    {
        throw new NotImplementedException();
    }

    public async Task<(bool result, T dialog)> ShowDialogAsync<T>(params (string key, object value)[] data) where T : BaseViewModel
    {
        var parameters = data.ToDictionary(kv => kv.key, kv => kv.value);
        var target = _serviceProvider.GetService<T>();
        target!.OnNavigatedToModal(parameters);
        if (target is LoginViewModel login)
        {
            await login.LoginCommand.ExecuteAsync(null);
        }
        else
        {
            target.SetModalComplete(false);
        }
        var result = await target.ModalCompletionTask;
        return (result, target);
    }
}
