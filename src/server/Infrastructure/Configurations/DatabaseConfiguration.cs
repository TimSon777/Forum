using System.Data;
using Application.Abstractions;
using Dapper.FluentMap;
using Infrastructure;
using Infrastructure.EntityMaps;
using Infrastructure.Implementations;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Npgsql;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class DatabaseConfiguration
{
    public static IServiceCollection AddForumDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connection = configuration.GetSettings<DatabaseSettings>().ToString(DatabaseType.Postgres);

        services.AddScoped<IDbConnection>(_ => new NpgsqlConnection(connection));

        FluentMapper.Initialize(configurator =>
        {
            configurator.AddMap(new UserMap());
            configurator.AddMap(new MessageMap());
        });

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

    public static IServiceCollection AddForumRepository(this IServiceCollection services)
    {
        services.AddScoped<IForumRepository, ForumRepository>();
        return services;
    }

    public static IServiceCollection AddMessageRepository(this IServiceCollection services)
    {
        services.AddScoped<IMessageRepository, MessageRepository>();
        return services;
    }

    public static IServiceCollection AddMetadataRepository(this IServiceCollection services)
    {
        services.AddScoped<IMetadataRepository, MetadataRepository>();
        return services;
    }
}