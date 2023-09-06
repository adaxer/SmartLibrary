using Microsoft.Extensions.Logging;
using SmartLibrary.Common.Interfaces;
//using SmartLibrary.Platforms;

namespace SmartLibrary;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
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

        builder.Services.AddSingleton<ILocationService, MauiLocationService>();
        builder.Services.AddSingleton<INavigationService, MauiNavigationService>();
        builder.Services.AddSingleton<IBookService, BookService>();
        builder.Services.AddSingleton<IRestService, RestService>();

        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddSingleton<MainPage>();

        builder.Services.AddTransient<SampleDataService>();
        builder.Services.AddTransient<DetailsViewModel>();
        builder.Services.AddTransient<DetailsPage>();

        builder.Services.AddSingleton<SearchViewModel>();

        builder.Services.AddSingleton<SearchPage>();

        builder.Services.AddSingleton<NewsViewModel>();

        builder.Services.AddSingleton<NewsPage>();

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
