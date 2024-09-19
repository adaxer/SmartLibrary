﻿using System;
using System.Linq;
using System.Threading.Tasks;
using SmartLibrary.Avalonia.ViewModels;
using SmartLibrary.Common.ViewModels;

namespace SmartLibrary.Avalonia;

internal class Automate
{
    private readonly ShellViewModel _shell;

    public Automate(ShellViewModel shell)
    {
        _shell = shell;
    }
    internal async void StartAsync()
    {
        await StartAutomationAsync();
    }

    private async Task StartAutomationAsync()
    {
        await Task.Delay(1000);
        var search = await WaitForAsync<MenuEntry?>(()=> _shell.Modules.Single(m => m.TargetType.Equals(typeof(SearchViewModel))));
        _shell.ShowItemCommand.Execute(search);
       
        SearchViewModel searchViewModel = await WaitForAsync<SearchViewModel>(() => (_shell.CurrentModule as SearchViewModel)!);
        searchViewModel.SearchText = "blazor";
        searchViewModel.SearchCommand.Execute(null);
    }

    async Task<T> WaitForAsync<T>(Func<T> check, int waitMs = 100)
    {
        T? result = check();
        while(result == null)
        {
            await Task.Delay(waitMs);
            result = check();
        }
        return result;
    }
}
