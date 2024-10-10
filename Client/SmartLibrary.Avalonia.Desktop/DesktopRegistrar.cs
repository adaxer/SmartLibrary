using Microsoft.Extensions.DependencyInjection;
using SmartLibrary.Avalonia.Desktop.Platform;
using SmartLibrary.Avalonia.Interfaces;
using SmartLibrary.Common.Interfaces;

namespace SmartLibrary.Avalonia.Desktop;
public class DesktopRegistrar : IRegisterServices
{
    void IRegisterServices.Register(IServiceCollection services)
    {
        services.AddSingleton<IDialogService, DesktopDialogService>();
        services.AddSingleton<INavigationService, DesktopNavigationService>();
    }
}
