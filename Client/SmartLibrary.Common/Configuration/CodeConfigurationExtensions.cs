using Microsoft.Extensions.Configuration;

public static class CodeConfigurationExtensions
{
    public static IConfigurationBuilder AddCodeConfiguration(this IConfigurationBuilder builder)
    {
        return builder.Add(new ConfigurationSource());
    }
}
