using Microsoft.Extensions.DependencyInjection;

namespace SmartLibrary.Avalonia.Interfaces;

public interface IRegisterServices
{
    void Register(IServiceCollection services) { }
}
