using System.Data;
using Chat.DAL.Abstractions.Chat;
using Chat.DAL.Implementations.Database.Repositories;
using Chat.Shared.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class DatabaseConfiguration
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration, ILogger logger)
    {
        var connection = configuration
            .Get<PostgresSettings>(PostgresSettings.Position)
            .ToString();
        
        logger.LogInformation("Db connection: {con}", connection);
        
        services.AddScoped<IDbConnection>(_ => new NpgsqlConnection(connection));
        return services;
    }
    
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IChatRepository, ChatRepository>();
        return services;
    }
}