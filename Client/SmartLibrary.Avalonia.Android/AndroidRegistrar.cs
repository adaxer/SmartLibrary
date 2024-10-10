using Microsoft.Extensions.DependencyInjection;
using SmartLibrary.Avalonia.Android.Platform;
using SmartLibrary.Avalonia.Interfaces;
using SmartLibrary.Common.Interfaces;

namespace SmartLibrary.Avalonia.Android;
public class AndroidRegistrar : IRegisterServices
{
    public int ExecutionOrder => 10;

    void IRegisterServices.Register(IServiceCollection services)
    {
        services.AddSingleton<IDialogService, AndroidDialogService>();
        services.AddSingleton<INavigationService, AndroidNavigationService>();
    }
}
