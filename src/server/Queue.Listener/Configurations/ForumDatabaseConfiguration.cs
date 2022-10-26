using Microsoft.EntityFrameworkCore;
using Queue.Listener;
using Queue.Listener.Forum.Database;
using Queue.Listener.Forum.Database.Repositories.Abstractions;
using Queue.Listener.Forum.Database.Repositories.Implementations;
using Shared.Settings;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ForumDatabaseConfiguration
{
    public static IServiceCollection AddForumDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration.Get<PostgresSettings>(PostgresSettings.Position);

        services.AddDbContext<ForumDbContext>(options =>
        {
            options.UseNpgsql(settings);
        });

        services.AddScoped<IForumRepository, ForumRepository>();

        return services;
    }
}