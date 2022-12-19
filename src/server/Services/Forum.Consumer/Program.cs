using Forum.Consumer;

await Host
    .CreateDefaultBuilder(args)
    .ConfigureServices((ctx, services) =>
    {
        var configuration = ctx.Configuration;
        services.AddRabbitMq(configuration,
            configurator => configurator.AddConsumer<MessageSaverConsumer>());
        services.AddForumDatabase(configuration);
        services.AddForumRepository();
    })
    .Build()
    .RunAsync();