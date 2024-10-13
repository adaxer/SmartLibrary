using Microsoft.Extensions.Configuration;

public static class IOSConfigurationExtensions
{
    public static IConfigurationBuilder AddIOSConfiguration(this IConfigurationBuilder builder)
    {
        return builder.Add(new IOSConfigurationSource());
    }
}
