using System;
using System.Linq;
using System.Threading.Tasks;
using SmartLibrary.Avalonia.Interfaces;
using SmartLibrary.Avalonia.ViewModels;
using SmartLibrary.Common.Interfaces;
using SmartLibrary.Common.ViewModels;

namespace SmartLibrary.Avalonia.Android;

internal class AutomateAndroid : IAutomate
{
    private readonly ShellViewModel _shell;
    private readonly IThemeService _themeService;

    public AutomateAndroid(IShellViewModel shell, IThemeService themeService)
    {
        _shell = (shell as ShellViewModel)!;
        _themeService = themeService;
    }
    public async void StartAsync()
    {
        await StartAutomationAsync();
    }

    private async Task StartAutomationAsync()
    {
        await Task.Delay(2000);

        _themeService.ChangeTheme("ThemeLight");

        var search = await WaitForAsync<MenuEntry?>(() => _shell.Modules.Single(m => m.TargetType.Equals(typeof(SearchViewModel))));
        _shell.ShowItemCommand.Execute(search);

        var searchViewModel = await WaitForAsync(() => (_shell.CurrentModule as SearchViewModel)!);
        searchViewModel.SearchText = "Gandalf";
        searchViewModel.SearchCommand.Execute(null);
        await Task.Delay(1000);

        var book = await WaitForAsync(() => searchViewModel.Books.FirstOrDefault());
        await Task.Delay(1000);
        searchViewModel.GoToDetailsCommand.Execute(book);
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
