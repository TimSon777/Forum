await Host
    .CreateDefaultBuilder(args)
    .ConfigureServices((ctx, services) =>
    {
        var configuration = ctx.Configuration;

        services.AddDatabase(configuration);
        services.AddRepositories();
        services.AddFileServices();
        services.AddFileAndMetadataSaverConsumer(configuration);
        services.AddCache(configuration);
    })
    .Build()
    .RunAsync();