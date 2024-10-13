using Microsoft.Extensions.Configuration;

public class AndroidConfigurationSource : IConfigurationSource
{
    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new AndroidConfigurationProvider();
    }
}
