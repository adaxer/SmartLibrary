using Microsoft.Extensions.Configuration;

public class IOSConfigurationSource : IConfigurationSource
{
    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new IOSConfigurationProvider();
    }
}
