// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.Configuration;

// ReSharper disable once InconsistentNaming
public static class IConfigurationExtensions
{
    public static TSettings GetSettings<TSettings>(this IConfiguration configuration)
        where TSettings : ISettings
    {
        return configuration
                   .GetRequiredSection(TSettings.SectionName)
                   .Get<TSettings>()
               ?? throw new InvalidOperationException($"Couldn't create setting {nameof(TSettings)} by section name {TSettings.SectionName}");
    }

    public static string GetString(this IConfiguration configuration, string position)
    {
        return configuration[position]
               ?? throw new InvalidOperationException($"Not found string by section name: {position}");
    }
}
