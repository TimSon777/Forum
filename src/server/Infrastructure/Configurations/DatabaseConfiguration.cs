using Application.Abstractions;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class DatabaseConfiguration
{
    public static IServiceCollection AddForumDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connection = configuration.GetSettings<DatabaseSettings>().ToString(DatabaseType.Postgres);

        services.AddDbContext<ForumDbContext>(options => options.UseNpgsql(connection));

        return services;
    }

    public static IServiceCollection AddMetadataDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration.GetSettings<DatabaseSettings>();
        var client = new MongoClient(settings.ToString(DatabaseType.Mongo));
        services.AddSingleton<IMongoClient>(client);
        services.AddScoped<IMongoDatabase>(sp => sp
            .GetRequiredService<IMongoClient>()
            .GetDatabase(settings.DatabaseName));

        return services;
    }

    public static IServiceCollection AddForumRepositories(this IServiceCollection services)
    {
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        
        return services;
    }

    public static IServiceCollection AddMetadataRepository(this IServiceCollection services)
    {
        services.AddScoped<IMetadataRepository, MetadataRepository>();
        return services;
    }
}