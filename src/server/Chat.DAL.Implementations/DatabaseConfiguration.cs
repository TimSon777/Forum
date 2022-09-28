﻿using System.Data;
using Chat.DAL.Abstractions.Chat;
using Chat.DAL.Implementations.Database.Repositories;
using Microsoft.Extensions.Configuration;
using Npgsql;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class DatabaseConfiguration
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connection = configuration.GetConnectionStringWithUpperCase("CHAT_DATABASE_CONNECTION");
        services.AddScoped<IDbConnection>(_ => new NpgsqlConnection(connection));
        return services;
    }
    
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IChatRepository, ChatRepository>();
        return services;
    }
}