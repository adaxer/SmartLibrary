using System;
using System.Linq;
using System.Threading.Tasks;
using SmartLibrary.Avalonia.Interfaces;
using SmartLibrary.Avalonia.ViewModels;
using SmartLibrary.Common.ViewModels;

namespace SmartLibrary.Avalonia.Desktop;

internal class AutomateDesktop : IAutomate
{
    private readonly ShellViewModel _shell;

    public AutomateDesktop(IShellViewModel shell)
    {
        _shell = (shell as ShellViewModel)!;
    }
    public async void StartAsync()
    {
        await StartAutomationAsync();
    }

    private async Task StartAutomationAsync()
    {
        await Task.Delay(1000);
        return;
        var search = await WaitForAsync<MenuEntry?>(() => _shell.Modules.Single(m => m.TargetType.Equals(typeof(SearchViewModel))));
        _shell.ShowItemCommand.Execute(search);

        var searchViewModel = await WaitForAsync(() => (_shell.CurrentModule as SearchViewModel)!);
        searchViewModel.SearchText = "blazor";
        searchViewModel.SearchCommand.Execute(null);
        await WaitForAsync(() => searchViewModel.Books.Any());
        await Task.Delay(1000);

        var login = await WaitForAsync<MenuEntry?>(()=> _shell.Dialogs.Single(m=>m.TargetType.Equals(typeof(LoginViewModel))));
        _shell.ShowItemCommand.Execute(login);
    }

    async Task<T> WaitForAsync<T>(Func<T> check, int waitMs = 100)
    {
        var result = check();
        while (result == null)
        {
            await Task.Delay(waitMs);
            result = check();
        }
        return result;
    }
}
