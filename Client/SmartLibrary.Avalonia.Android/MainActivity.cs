using Android.App;
using Android.Content.PM;
using Avalonia;
using Avalonia.Android;
using Projektanker.Icons.Avalonia.MaterialDesign;
using Projektanker.Icons.Avalonia;
using SmartLibrary.Avalonia.Services;
using Android.OS;

namespace SmartLibrary.Avalonia.Android;
[Activity(
    Label = "SmartLibrary.Avalonia.Android",
    Theme = "@style/MyTheme.NoActionBar",
    Icon = "@drawable/icon",
    MainLauncher = true,
    ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode)]
public class MainActivity : AvaloniaMainActivity<App>
{
    protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
    {
        IconProvider.Current
                    .Register<MaterialDesignIconProvider>()
                    .Register<CustomIconProvider>();

        return base.CustomizeAppBuilder(builder)
            .WithInterFont();
    }
}
