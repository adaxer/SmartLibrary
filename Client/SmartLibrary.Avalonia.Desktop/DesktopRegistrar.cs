using Microsoft.Extensions.DependencyInjection;
using SmartLibrary.Avalonia.Desktop.Platform;
using SmartLibrary.Avalonia.Desktop.Views;
using SmartLibrary.Avalonia.Interfaces;
using SmartLibrary.Common.Interfaces;

namespace SmartLibrary.Avalonia.Desktop;
public class DesktopRegistrar : IRegisterServices
{
    public int ExecutionOrder => 10;

    void IRegisterServices.Register(IServiceCollection services)
    {
        // Main View
        services.AddSingleton<IShellView, ShellWindow>();

        // Platform services
        services.AddSingleton<IDialogService, DesktopDialogService>();
        services.AddSingleton<INavigationService, DesktopNavigationService>();

        // Automate
#if AUTOMATE 
        services.AddSingleton<IAutomate, AutomateDesktop>();
#endif
    }
}
