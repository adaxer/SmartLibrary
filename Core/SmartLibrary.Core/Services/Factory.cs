using Microsoft.Extensions.DependencyInjection;

namespace SmartLibrary.Core.Services;
public class Factory<T> : IFactory<T>
{
    private readonly IServiceProvider _serviceProvider;

    public Factory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public T Create() => _serviceProvider.GetService<T>() ?? throw new NullReferenceException($"Cant create a {typeof(T).FullName} instance");
}
