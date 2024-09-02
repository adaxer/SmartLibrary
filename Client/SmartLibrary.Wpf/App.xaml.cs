using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SmartLibrary.Data;
using SmartLibrary.Wpf.Views;
using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using SmartLibrary.Wpf.Services;
using SmartLibrary.Core.Interfaces;
using SmartLibrary.Core.Services;
using SmartLibrary.Common.Services;
using SmartLibrary.Common.ViewModels;
using SmartLibrary.Common.Interfaces;
using SmartLibrary.Wpf.Interfaces;
using CommunityToolkit.Mvvm.Messaging;
using SmartLibrary.Core.Localization;

namespace SmartLibrary.Wpf;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly IHost _host;
    private IServiceProvider _container;

    public App()
    {
        Strings.Culture = new System.Globalization.CultureInfo("FR-fr");
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                ConfigureServices(services);
                _container = services.BuildServiceProvider();
            })
            .Build();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        // Add services here
        services.AddSingleton<INavigationService, WpfNavigatorService>();
        services.AddSingleton<IDialogService, DialogService>();
        services.AddSingleton<IMessenger, WeakReferenceMessenger>();
        services.AddSingleton<IBookService, BookService>();
		services.AddSingleton<ILocationService, WpfLocationService>();
		services.AddSingleton<IUserClient, UserClient>();
        // Bei dem Client wird BaseAddress nicht gesetzt, warum auch immer
        services.AddHttpClient(nameof(IBookStorage), client => client.BaseAddress = new Uri("https://localhost:7023", UriKind.Absolute));
        services.AddHttpClient<IUserClient, UserClient>(client => client.BaseAddress = new Uri("https://localhost:7023", UriKind.Absolute));
        services.AddHttpClient<IBookService, BookService>(client => client.BaseAddress = new Uri("https://www.googleapis.com", UriKind.Absolute));
        services.AddSingleton<ILocalizationService, ResXLocalizationService>();
        services.AddSingleton<IBookShareClient, BookShareClient>();
		services.AddSingleton<IBookStorage, BookStorage>();
		services.AddSingleton<ISecureStorage, WpfSecureStorage>();
        services.AddSingleton<IPubSubService,WpfPubSubService>();

        services.AddTransient(typeof(ModuleViewModel<>));
        services.AddSingleton<ShellViewModel>();
        services.AddSingleton<ShellWindow>();
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<MainView>();

        services.AddSingleton<SearchViewModel>();
        services.AddSingleton<SearchView>();

        services.AddTransient<DetailsViewModel>();
        services.AddTransient<DetailsView>();

        services.AddTransient<LoginViewModel>();

        services.AddSingleton<NewsViewModel>();
        services.AddSingleton<NewsView>();

        services.AddTransient(sp => sp.GetRequiredService<IBookShareClient>() as IRequireInitializeAsync);
        services.AddDbContext<BooksContext>(options =>
                options.UseSqlite("Filename=Books.db"));
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        _host.Start();
        LateInitialize();

        var mainWindow = _host.Services.GetRequiredService<ShellWindow>();
        mainWindow.Show();
        (mainWindow.DataContext as BaseViewModel)?.OnNavigatedTo();

        base.OnStartup(e);
    }

    private void LateInitialize()
    {
        _container.GetService<IEnumerable<IRequireInitializeAsync>>().ToList().ForEach(a=>a.InitializeAsync());
        _container.GetService<IEnumerable<IRequireInitialize>>().ToList().ForEach(i=>i.Initialize());
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        using (_host)
        {
            await _host.StopAsync(TimeSpan.FromSeconds(5));
        }

        base.OnExit(e);
    }
}
