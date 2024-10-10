using System;
using Microsoft.Extensions.DependencyInjection;
using SmartLibrary.Avalonia.Interfaces;
using SmartLibrary.Avalonia.ViewModels;
using SmartLibrary.Common.Interfaces;
using SmartLibrary.Common.Services;
using SmartLibrary.Common.ViewModels;
using SmartLibrary.Core.Interfaces;
using SmartLibrary.Core.Localization;
using SmartLibrary.Core.Services;

namespace SmartLibrary.Avalonia.Services;
public class ServiceRegistrar : IRegisterServices
{
    public int ExecutionOrder => 5;

    public void Register(IServiceCollection services)
    {
        // Platform ViewModels 
        services.AddSingleton<IShellViewModel, ShellViewModel>();
        services.AddTransient<SettingsViewModel>();

        // Common ViewModels
        services.AddTransient<WelcomeViewModel>();
        services.AddTransient<LoginViewModel>();
        services.AddTransient<AboutViewModel>();
        services.AddSingleton<SearchViewModel>();
        services.AddTransient<DetailsViewModel>();

        // Common Services
        services.AddTransient<IBookService, BookService>();
        services.AddTransient<IUserClient, UserClient>();
        services.AddTransient<ILocalizationService, ResXLocalizationService>();

        // Platform Services
        services.AddSingleton<IThemeService, AvaloniaThemeService>();

        services.AddSingleton<ILocationService, DummyLocationService>();
        services.AddSingleton<IBookStorage, DummyBookStorage>();
        services.AddSingleton<IBookShareClient, DummyBookShareClient>();
        services.AddSingleton<ISecureStorage, DummyStorage>();

        // Http Clients
        services.AddHttpClient<IUserClient, UserClient>(client => client.BaseAddress = new Uri("https://localhost:7023", UriKind.Absolute));
        services.AddHttpClient<IBookService, BookService>(client => client.BaseAddress = new Uri("https://www.googleapis.com", UriKind.Absolute));

        // From ext. Libraries
        services.AddSingleton<IPubSubService, AvaloniaPubSubService>();
    }
}
