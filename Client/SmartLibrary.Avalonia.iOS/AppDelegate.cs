﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.iOS;
using Avalonia.Media;
using Foundation;
using Projektanker.Icons.Avalonia.MaterialDesign;
using Projektanker.Icons.Avalonia;
using SmartLibrary.Avalonia.Services;
using UIKit;

namespace SmartLibrary.Avalonia.iOS;
// The UIApplicationDelegate for the application. This class is responsible for launching the 
// User Interface of the application, as well as listening (and optionally responding) to 
// application events from iOS.
[Register("AppDelegate")]
#pragma warning disable CA1711 // Identifiers should not have incorrect suffix
public partial class AppDelegate : AvaloniaAppDelegate<App>
#pragma warning restore CA1711 // Identifiers should not have incorrect suffix
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
