using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Microsoft.Extensions.DependencyInjection;
using SmartLibrary.Avalonia.Interfaces;
using SmartLibrary.Avalonia.ViewModels;
using SmartLibrary.Avalonia.Views;
using SmartLibrary.Common.ViewModels;

namespace SmartLibrary.Avalonia;
public partial class App : Application
{
    private ServiceProvider _serviceProvider = default!;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // If you use CommunityToolkit, line below is needed to remove Avalonia data validation.
        // Without this line you will get duplicate validations from both Avalonia and CT
        BindingPlugins.DataValidators.RemoveAt(0);

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new ShellWindow
            {
                DataContext = _serviceProvider.GetRequiredService<ShellViewModel>()
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new WelcomeView
            {
                DataContext = _serviceProvider.GetRequiredService<WelcomeViewModel>()
            };
        }

        base.OnFrameworkInitializationCompleted();

#if AUTOMATE
        _serviceProvider.GetRequiredService<Automate>().StartAsync();
#endif
    }

    public override void RegisterServices()
    {
        var services = new ServiceCollection();

        List<IRegisterServices> registrars = AppDomain.CurrentDomain
            .GetAssemblies()
            .Where(a => (a.FullName ?? string.Empty).Contains(this.GetType().Namespace ?? string.Empty))
            .SelectMany(a => a.GetTypes())
            .Where(t => typeof(IRegisterServices).IsAssignableFrom(t) && t.IsClass)
            .Select(Activator.CreateInstance)
            .OfType<IRegisterServices>()
            .ToList();

        foreach (var registrar in registrars)
        {
            try
            {
                registrar.Register(services);
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Could not register Services on {registrar}: {ex}");
            }
        }

        base.RegisterServices();

        _serviceProvider = services.BuildServiceProvider();
    }
}
