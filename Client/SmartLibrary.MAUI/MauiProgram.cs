using CommunityToolkit.Maui;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ISecureStorage = SmartLibrary.Common.Interfaces.ISecureStorage;

namespace SmartLibrary.MAUI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.SetMinimumLevel(LogLevel.Debug);
#else
		builder.Logging.SetMinimumLevel(LogLevel.Warning);
#endif

        builder.Services.AddSingleton<ILocalizationService, ResXLocalizationService>();
        builder.Services.AddSingleton<ILocationService, MauiLocationService>();
        builder.Services.AddSingleton<INavigationService, MauiNavigationService>();
        builder.Services.AddSingleton<IBookService, BookService>();
        builder.Services.AddSingleton<IUserClient, UserClient>();
        var configuration = new ConfigurationBuilder()
            .AddCodeConfiguration()
            .AddEnvironmentVariables()
            .Build();
        builder.Services.AddSingleton<IConfiguration>(configuration);
        var apiBase = configuration.GetValue<string>("ApiBaseUrl")!;
        builder.Services.AddHttpClient<IUserClient, UserClient>(client=>client.BaseAddress=new Uri(apiBase, UriKind.Absolute));
        builder.Services.AddHttpClient<IBookService, BookService>(client => client.BaseAddress = new Uri("https://www.googleapis.com", UriKind.Absolute));
        builder.Services.AddHttpClient<IBookStorage, BookStorage>(client => client.BaseAddress = new Uri(apiBase, UriKind.Absolute));

        builder.Services.AddTransient<IBookStorage, BookStorage>();
        builder.Services.AddTransient<ISecureStorage, MAUISecureStorage>();
        builder.Services.AddSingleton<IBookShareClient, BookShareClient>();
        builder.Services.AddSingleton<IPubSubService, PubSubService>();

        builder.Services.AddSingleton<WelcomeViewModel>();
        builder.Services.AddSingleton<WelcomePage>();

        builder.Services.AddTransient<DetailsViewModel>();
        builder.Services.AddTransient<DetailsPage>();

        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<LoginPage>();

        builder.Services.AddSingleton<SearchViewModel>();
        builder.Services.AddSingleton<SearchPage>();

        builder.Services.AddSingleton<NewsViewModel>();
        builder.Services.AddSingleton<NewsPage>();

        builder.Services.AddSingleton(sp => (sp.GetRequiredService<IBookShareClient>() as IRequireInitializeAsync)!);

        builder.Services.AddDbContext<BooksContext>(options =>
                    options.UseSqlite("Filename=Books.db"));

        // Platform-Abhängigkeiten
        //#if WINDOWS
        //        builder.Services.AddSingleton<IPlatform, WinPlatform>();
        //#elif ANDROID
        //        builder.Services.AddSingleton<IPlatform, DroidPlatform>();
        //#endif

        // Damit ist das obige unnötig
        new Registrar().RegisterDependencies(builder.Services);

        return builder.Build();

    }
}
