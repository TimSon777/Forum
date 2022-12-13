await Host
    .CreateDefaultBuilder(args)
    .ConfigureServices((ctx, services) =>
    {
        var configuration = ctx.Configuration;
        services.AddForumDatabase(configuration);
        services.AddForumRepository();
    })
    .Build()
    .RunAsync();