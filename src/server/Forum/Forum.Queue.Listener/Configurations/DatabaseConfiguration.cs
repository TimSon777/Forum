using Forum.Queue.Listener;
using Forum.Queue.Listener.Abstractions;
using Forum.Queue.Listener.Implementations;
using Microsoft.EntityFrameworkCore;
using Shared.Settings;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class DatabaseConfiguration
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration.Get<DatabaseSettings>(DatabaseSettings.Position)
            ?? throw new InvalidOperationException();

        services.AddDbContext<ForumDbContext>(options => options.UseNpgsql(settings.ToString()));
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IForumRepository, ForumRepository>();
        return services;
    }
}