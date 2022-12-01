await Host
    .CreateDefaultBuilder(args)
    .ConfigureServices((ctx, services) =>
    {
        var configuration = ctx.Configuration;
        services.AddDatabase(configuration);
        services.AddRepositories();
        services.AddMessageSaverConsumer(configuration);
    })
    .Build()
    .RunAsync();