using MinimalApi.Endpoint.Extensions;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

services.AddCors();
services.AddControllers();
services.AddEndpoints();
services.AddCache(configuration);
services.AddCachingService();
services.AddSwaggerGen();
services.AddEndpointsApiExplorer();
services.AddAws(configuration);
services.AddFileProvider();
services.AddBucketCreator();
services.AddRabbitMq(configuration);
services.AddBucketCreatorBackgroundService();

var app = builder.Build();

app.UseCors(options => options
    .AllowCredentials()
    .AllowAnyHeader()
    .AllowAnyMethod()
    .WithOrigins(configuration.GetString("ORIGIN:FRONT")));

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.MapEndpoints();
app.Run();