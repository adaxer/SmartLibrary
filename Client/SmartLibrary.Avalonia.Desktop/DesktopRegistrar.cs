using Microsoft.Extensions.Configuration;
using System;
using Microsoft.Extensions.DependencyInjection;
using SmartLibrary.Avalonia.Desktop.Platform;
using SmartLibrary.Avalonia.Desktop.Views;
using SmartLibrary.Avalonia.Interfaces;
using SmartLibrary.Common.Interfaces;
using SmartLibrary.Common.Services;

namespace SmartLibrary.Avalonia.Desktop;
public class DesktopRegistrar : IRegisterServices
{
    public int ExecutionOrder => 10;

    public void Register(IServiceCollection services)
    {
        // Configuration
        var configuration = AddConfiguration(services);
        services.AddSingleton(configuration);


        // Main View
        services.AddSingleton<IShellView, ShellWindow>();

        // Platform services
        services.AddSingleton<IDialogService, DesktopDialogService>();
        services.AddSingleton<INavigationService, DesktopNavigationService>();

        // HttpClient
        var apiBaseUrl = configuration.GetValue<string>("ApiBaseUrl") ?? throw new ArgumentException("Configuration must define ApiBaseUrl");
        services.AddHttpClient<IUserClient, UserClient>(client => client.BaseAddress = new Uri(apiBaseUrl, UriKind.Absolute));

        // Automate
#if AUTOMATE 
        services.AddSingleton<IAutomate, AutomateDesktop>();
#endif
    }

    private IConfiguration AddConfiguration(IServiceCollection services)
    {
        string environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Development";
        if (OperatingSystem.IsAndroid() || OperatingSystem.IsIOS())
        {
            environment = "Testing";
        }
        var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        return configuration;
    }
}
