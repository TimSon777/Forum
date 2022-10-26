await Host
    .CreateDefaultBuilder(args)
    .ConfigureServices()
    .Build()
    .RunAsync();