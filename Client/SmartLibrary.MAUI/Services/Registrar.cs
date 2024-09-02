namespace SmartLibrary.MAUI;

public partial class Registrar
{
    public void RegisterDependencies(IServiceCollection services) => RegisterInternal(services);

    partial void RegisterInternal(IServiceCollection services);
}