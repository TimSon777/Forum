using MassTransit;
using Microsoft.Extensions.Configuration;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

// ReSharper disable once InconsistentNaming
public static class IServiceCollectionExtensions
{
    public static IServiceCollection ConfigureSettings<TSettings>(this IServiceCollection services, 
        IConfiguration configuration) 
        where TSettings : class, ISettings
    {
        var section = configuration.GetRequiredSection(TSettings.SectionName);
        services.Configure<TSettings>(section);
        return services;
    }

    public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration,
        Action<IBusRegistrationConfigurator>? configure = null)
    {
        var settings = configuration.GetSettings<BrokerConnectionSettings>();

        services.AddMassTransit(configurator =>
        {
            configure?.Invoke(configurator);

            configurator.UsingRabbitMq((ctx, brokerConfigurator) =>
            {
                brokerConfigurator.Host(settings.Host, "/", hostConfigurator =>
                {
                    hostConfigurator.Password(settings.Password);
                    hostConfigurator.Username(settings.UserName);
                });
                
                brokerConfigurator.ConfigureEndpoints(ctx);
            });
        });

        return services;
    }
}