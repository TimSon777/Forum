using MinimalApi.Endpoint.Extensions;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddSwaggerGen();
services.AddEndpointsApiExplorer();

services.AddCache(configuration);
services.AddMetadataDatabase(configuration);
services.AddCollectionCreatorBackgroundService();
services.AddRabbitMq(configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapEndpoints();
app.Run();