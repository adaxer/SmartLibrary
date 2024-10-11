using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using SmartLibrary.Avalonia.Interfaces;

namespace SmartLibrary.Avalonia;
public partial class App : Application
{
    private ServiceProvider _serviceProvider = default!;

    public ServiceProvider? ServiceProvider => _serviceProvider;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // If you use CommunityToolkit, line below is needed to remove Avalonia data validation.
        // Without this line you will get duplicate validations from both Avalonia and CT
        BindingPlugins.DataValidators.RemoveAt(0);

        // Set the proper MainView/Window depending on the Application lifetime
        try
        {
            var shell = _serviceProvider.GetRequiredService<IShellViewModel>();
            var view = _serviceProvider.GetRequiredService<IShellView>();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop && view is Window window)
            {
                desktop.MainWindow = window;
            }
            else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform && view is Control control)
            {
                singleViewPlatform.MainView = control;
            }
        }
        catch (Exception ex)
        {
            Trace.TraceError($"Could not set main view. Make sure to register an IShellViewModel and an IShellView being a Window in desktop mode resp. a Control in mobile mode: {ex}");
        }

        base.OnFrameworkInitializationCompleted();

        if(_serviceProvider.GetService<IAutomate>() is IAutomate auto)
        {
            auto.StartAsync();
        }
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
            .OrderBy(r => r.ExecutionOrder)
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
