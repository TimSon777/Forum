using Forum.API;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ChatConfigurations
{
    public static IServiceCollection AddChat(this IServiceCollection services)
    {
        services.AddScoped<IChatConnector, ChatConnector>();
        return services;
    }
}