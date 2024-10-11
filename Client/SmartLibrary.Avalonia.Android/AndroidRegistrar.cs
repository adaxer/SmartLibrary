using Microsoft.Extensions.DependencyInjection;
using SmartLibrary.Avalonia.Android.Platform;
using SmartLibrary.Avalonia.Android.ViewModels;
using SmartLibrary.Avalonia.Android.Views;
using SmartLibrary.Avalonia.Interfaces;
using SmartLibrary.Common.Interfaces;

namespace SmartLibrary.Avalonia.Android;
public class AndroidRegistrar : IRegisterServices
{
    public int ExecutionOrder => 10;

    void IRegisterServices.Register(IServiceCollection services)
    {
        // Shell
        services.AddSingleton<IShellViewModel, AndroidShellViewModel>();
        services.AddSingleton<IShellView, ShellView>();

        // Platform services
        services.AddSingleton<IDialogService, AndroidDialogService>();
        services.AddSingleton<INavigationService, AndroidNavigationService>();

        // Automate
#if AUTOMATE 
        services.AddSingleton<IAutomate, AutomateAndroid>();
#endif
    }
}
