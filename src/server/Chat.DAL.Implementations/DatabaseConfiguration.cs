using System.Data;
using Chat.DAL.Abstractions.Chat;
using Chat.DAL.Implementations.Database.Maps;
using Chat.DAL.Implementations.Database.Repositories;
using Chat.Shared.Settings;
using Dapper.FluentMap;
using Microsoft.Extensions.Configuration;
using Npgsql;

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