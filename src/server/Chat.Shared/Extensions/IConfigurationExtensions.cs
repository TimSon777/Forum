// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.Configuration;

// ReSharper disable once InconsistentNaming
public static class IConfigurationExtensions
{
    public static T Get<T>(this IConfiguration configuration, string position) 
        where T : new()
    {
        var obj = new T();
        
        configuration
            .GetSection(position)
            .Bind(obj);
        
        return obj;
    }
}