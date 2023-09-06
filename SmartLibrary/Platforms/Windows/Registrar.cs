using SmartLibrary.Platforms;

namespace SmartLibrary;

public partial class Registrar
{
    partial void RegisterInternal(IServiceCollection services)
    {
        services.AddSingleton<IPlatform, WinPlatform>();
    }
}