namespace SmartLibrary;

public partial class Registrar
{
    public void RegisterDependencies(IServiceCollection services) => RegisterInternal(services);

    partial void RegisterInternal(IServiceCollection services);
}