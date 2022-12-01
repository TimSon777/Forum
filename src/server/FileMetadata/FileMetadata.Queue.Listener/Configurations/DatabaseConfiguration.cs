using FileMetadata.Queue.Listener.Abstractions;
using FileMetadata.Queue.Listener.Implementations;
using MongoDB.Driver;
using Shared.Settings;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class DatabaseConfiguration
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration.Get<DatabaseSettings>(DatabaseSettings.Position);

        var connectionString = settings.ToString(DatabaseType.Mongo);
        services.AddScoped<IMongoDatabase>(_ =>
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(settings.DatabaseName) ?? throw new InvalidOperationException("Mongoi");
            return database;
        });

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IMetadataRepository, MetadataRepository>();
        return services;
    }
}