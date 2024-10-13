using Microsoft.Extensions.Configuration;

public class ConfigurationSource : IConfigurationSource
{
    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new CodeConfigurationProvider();
    }
}
