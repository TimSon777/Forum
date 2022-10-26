using System.Data;
using Dapper.FluentMap;
using Forum.DAL.Abstractions.Chat;
using Forum.DAL.Implementations.Database.Maps;
using Forum.DAL.Implementations.Database.Repositories;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Shared.Settings;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class DatabaseConfiguration
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connection = configuration
            .Get<PostgresSettings>(PostgresSettings.Position)
            .ToString();

        services.AddScoped<IDbConnection>(_ => new NpgsqlConnection(connection));

        FluentMapper.Initialize(configurator =>
        {
            configurator.AddMap(new UserMap());
            configurator.AddMap(new MessageMap());
        });
        
        return services;
    }
    
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IChatRepository, ChatRepository>();
        return services;
    }
}