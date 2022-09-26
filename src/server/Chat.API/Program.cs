var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

services.AddMessageConsumer(configuration, 10);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();