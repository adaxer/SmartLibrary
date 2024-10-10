using Microsoft.Extensions.DependencyInjection;
using SmartLibrary.Avalonia.Android.Platform;
using SmartLibrary.Avalonia.Interfaces;
using SmartLibrary.Common.Interfaces;

namespace SmartLibrary.Avalonia.Android;
public class AndroidRegistrar : IRegisterServices
{
    void IRegisterServices.Register(IServiceCollection services)
    {
        services.AddSingleton<IDialogService, AndroidDialogService>();
        services.AddSingleton<INavigationService, AndroidNavigationService>();
    }
}
