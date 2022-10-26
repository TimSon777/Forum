// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class DiConfigurations
{
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var cfg = builder.Configuration;
        
        services.AddSwagger();
        services.AddControllers();
        services.AddAws(cfg);
        services.AddFileProvider();
        return builder;
    }
}