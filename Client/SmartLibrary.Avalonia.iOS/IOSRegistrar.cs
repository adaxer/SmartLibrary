using Microsoft.Extensions.DependencyInjection;
using SmartLibrary.Avalonia.Interfaces;
using SmartLibrary.Avalonia.iOS;
using SmartLibrary.Avalonia.iOS.Platform;
using SmartLibrary.Avalonia.iOS.ViewModels;
using SmartLibrary.Avalonia.iOS.Views;
using SmartLibrary.Common.Interfaces;

namespace SmartLibrary.Avalonia.IOS;
public class IOSRegistrar : IRegisterServices
{
    public int ExecutionOrder => 10;

    void IRegisterServices.Register(IServiceCollection services)
    {
        // Shell
        services.AddSingleton<IShellViewModel, IOSShellViewModel>();
        services.AddSingleton<IShellView, ShellView>();

        // Platform services
        services.AddSingleton<IDialogService, IOSDialogService>();
        services.AddSingleton<INavigationService, IOSNavigationService>();

        // Automate
#if AUTOMATE 
        services.AddSingleton<IAutomate, AutomateIOS>();
#endif
    }
}
