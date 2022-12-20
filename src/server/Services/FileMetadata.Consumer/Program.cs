using FileMetadata.Consumer;

await Host
    .CreateDefaultBuilder(args)
    .ConfigureServices((ctx, services) =>
    {
        var configuration = ctx.Configuration;

        services.AddMetadataRepository();
        services.AddFileMover();
        services.AddAws(configuration);
        services.AddRabbitMq(configuration,
            configure => configure.AddConsumer<FileAndMetadataSaverConsumer>());
        
        services.AddCache(configuration);
        services.AddCachingService();
        services.AddMetadataDatabase(configuration);
    })
    .Build()
    .RunAsync();