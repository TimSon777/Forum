using System.Data;
using Dapper.FluentMap;
using Forum.Application.Repositories;
using Forum.Infrastructure.EntityMaps;
using Forum.Infrastructure.Repositories;
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
            .Get<DatabaseSettings>(DatabaseSettings.Position)
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
        services.AddScoped<IMessageRepository, MessageRepository>();
        return services;
    }
}