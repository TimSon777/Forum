// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ServicesConfiguration
{
    public static IHostBuilder ConfigureServices(this IHostBuilder builder)
    {
        builder.ConfigureServices((ctx, services) =>
        {
            var configuration = ctx.Configuration;
            services.AddForumDatabase(configuration);
            services.AddMessageSaverConsumer(configuration);
        });

        return builder;
    }
}