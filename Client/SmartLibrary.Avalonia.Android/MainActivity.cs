using Android.App;
using Android.Content.PM;
using Avalonia;
using Avalonia.Android;
using Projektanker.Icons.Avalonia.MaterialDesign;
using Projektanker.Icons.Avalonia;
using SmartLibrary.Avalonia.Services;
using SmartLibrary.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace SmartLibrary.Avalonia.Android;
[Activity(
    Label = "SmartLibrary.Avalonia.Android",
    Theme = "@style/MyTheme.NoActionBar",
    Icon = "@drawable/icon",
    MainLauncher = true,
    ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode)]
public class MainActivity : AvaloniaMainActivity<App>
{
    INavigationService? _navigationService=default;

    protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
    {
        IconProvider.Current
                    .Register<MaterialDesignIconProvider>()
                    .Register<CustomIconProvider>();

        return base.CustomizeAppBuilder(builder)
            .WithInterFont();
    }

    INavigationService? GetNavigationService()
    {
        try
        {
            if (_navigationService == null && global::Avalonia.Application.Current is App app)
            {
                _navigationService = app.ServiceProvider!.GetService<INavigationService>();
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Trace.TraceError($"Could not retrieve INavigationService: {ex}");
        }
        return _navigationService;
    }

    public override async void OnBackPressed()
    {
        if(GetNavigationService() is INavigationService navigationService)
        {
            if(navigationService.CanGoBack())
            {
                await navigationService.GoBack();
            }
        }
    }
}
