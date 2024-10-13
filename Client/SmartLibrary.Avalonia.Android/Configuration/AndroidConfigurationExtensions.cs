using Microsoft.Extensions.Configuration;

public static class AndroidConfigurationExtensions
{
    public static IConfigurationBuilder AddAndroidConfiguration(this IConfigurationBuilder builder)
    {
        return builder.Add(new AndroidConfigurationSource());
    }
}
