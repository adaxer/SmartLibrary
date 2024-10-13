using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartLibrary.Avalonia.Interfaces;
using SmartLibrary.Avalonia.iOS;
using SmartLibrary.Avalonia.iOS.Platform;
using SmartLibrary.Avalonia.iOS.ViewModels;
using SmartLibrary.Avalonia.iOS.Views;
using SmartLibrary.Common.Interfaces;
using SmartLibrary.Common.Services;

namespace SmartLibrary.Avalonia.IOS;
public class IOSRegistrar : IRegisterServices
{
    public int ExecutionOrder => 10;

    void IRegisterServices.Register(IServiceCollection services)
    {
        // Configuration
        var configuration = AddConfiguration(services);
        services.AddSingleton(configuration);

        // Shell
        services.AddSingleton<IShellViewModel, IOSShellViewModel>();
        services.AddSingleton<IShellView, ShellView>();

        // Platform services
        services.AddSingleton<IDialogService, IOSDialogService>();
        services.AddSingleton<INavigationService, IOSNavigationService>();

        // HttpClient
        var apiBaseUrl = configuration.GetValue<string>("ApiBaseUrl") ?? throw new ArgumentException("Configuration must define ApiBaseUrl");
        services.AddHttpClient<IUserClient, UserClient>(client => client.BaseAddress = new Uri(apiBaseUrl, UriKind.Absolute));

        // Automate
#if AUTOMATE 
        services.AddSingleton<IAutomate, AutomateIOS>();
#endif
    }

    private IConfiguration AddConfiguration(IServiceCollection services)
    {
        var configuration = new ConfigurationBuilder()
                .AddCodeConfiguration()
                .AddEnvironmentVariables()
                .Build();
        return configuration;
    }
}
