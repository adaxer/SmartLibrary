using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartLibrary.Avalonia.Android.Platform;
using SmartLibrary.Avalonia.Android.ViewModels;
using SmartLibrary.Avalonia.Android.Views;
using SmartLibrary.Avalonia.Interfaces;
using SmartLibrary.Common.Interfaces;
using SmartLibrary.Common.Services;

namespace SmartLibrary.Avalonia.Android;
public class AndroidRegistrar : IRegisterServices
{
    public int ExecutionOrder => 10;

    public  void Register(IServiceCollection services)
    {
        // Configuration
        var configuration = AddConfiguration(services);
        services.AddSingleton(configuration);

        // Shell
        services.AddSingleton<IShellViewModel, AndroidShellViewModel>();
        services.AddSingleton<IShellView, ShellView>();

        // Platform services
        services.AddSingleton<IDialogService, AndroidDialogService>();
        services.AddSingleton<INavigationService, AndroidNavigationService>();

        // HttpClient
        var apiBaseUrl = configuration.GetValue<string>("ApiBaseUrl") ?? throw new ArgumentException("Configuration must define ApiBaseUrl");
        services.AddHttpClient<IUserClient, UserClient>(client => client.BaseAddress = new Uri(apiBaseUrl, UriKind.Absolute));

        // Automate
#if AUTOMATE 
        services.AddSingleton<IAutomate, AutomateAndroid>();
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
