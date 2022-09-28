﻿using System.Reflection;
using Chat.Shared.Settings;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var builder = new ConfigurationBuilder();

builder.SetBasePath(Directory.GetCurrentDirectory());

// ReSharper disable once StringLiteralTypo
builder.AddJsonFile("appsettings.json");
builder.AddEnvironmentVariables();

var configurationRoot = builder.Build();
var connection = configurationRoot
    .Get<PostgresSettings>(PostgresSettings.Position)
    .ToString();

var logger = new LoggerFactory().CreateLogger<Program>();
logger.LogInformation("Db connection: {con}", connection);

using var serviceProvider = new ServiceCollection()
    .AddFluentMigratorCore()
    .ConfigureRunner(rb => rb
        .AddPostgres()
        .WithGlobalConnectionString(connection)
        .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations())
    .AddLogging(lb => lb.AddFluentMigratorConsole())
    .BuildServiceProvider(false);

using var scope = serviceProvider.CreateScope();

var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

runner.MigrateUp();