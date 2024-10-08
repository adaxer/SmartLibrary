using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging;
using NSubstitute;
using SmartLibrary.Avalonia;
using SmartLibrary.Avalonia.Services;
using SmartLibrary.Avalonia.ViewModels;
using SmartLibrary.Common.Interfaces;
using SmartLibrary.Common.Models;
using SmartLibrary.Common.Services;
using SmartLibrary.Common.ViewModels;
using SmartLibrary.Core.Interfaces;
using SmartLibrary.Core.Localization;
using SmartLibrary.Core.Services;

namespace Microsoft.Extensions.DependencyInjection;
public static class ServiceCollectionExtensions
{
    public static void AddDependencies(this IServiceCollection services)
    {
        // Platform ViewModels 
        services.AddSingleton<ShellViewModel>();
        services.AddTransient<SettingsViewModel>();

        // Common ViewModels
        services.AddTransient<MainViewModel>();
        services.AddTransient<LoginViewModel>();
        services.AddTransient<AboutViewModel>();
        services.AddSingleton<SearchViewModel>();
        services.AddTransient<DetailsViewModel>();

        // Common Services
        services.AddTransient<IBookService, BookService>();
        services.AddTransient<IUserClient, UserClient>();
        services.AddTransient<ILocalizationService, ResXLocalizationService>();

        // Platform Services
        services.AddSingleton<IDialogService, AvaloniaDialogService>();
        services.AddSingleton<INavigationService, AvaloniaNavigationService>();
        services.AddSingleton<IThemeService, AvaloniaThemeService>();

        services.AddSingleton<ILocationService, DummyLocationService>();
        services.AddSingleton<IBookStorage, DummyBookStorage>();
        services.AddSingleton<IBookShareClient, DummyBookShareClient>();
        services.AddSingleton<ISecureStorage, DummyStorage>();

        // Http Clients
        services.AddHttpClient<IUserClient, UserClient>(client => client.BaseAddress = new Uri("https://localhost:7023", UriKind.Absolute));
        services.AddHttpClient<IBookService, BookService>(client => client.BaseAddress = new Uri("https://www.googleapis.com", UriKind.Absolute));

        // From ext. Libraries
        services.AddSingleton<IMessenger, WeakReferenceMessenger>();

        // AUTOMATE
#if AUTOMATE
        services.AddSingleton<Automate>();
#endif
    }
}
