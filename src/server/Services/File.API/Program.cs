using MinimalApi.Endpoint.Extensions;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

services.AddCors();

services.AddSwaggerGen();
services.AddEndpointsApiExplorer();
services.AddAws(configuration);
services.AddFileProvider();
services.AddBucketCreator();
services.AddRabbitMq(configuration);
services.AddBucketCreatorBackgroundService();

var app =builder.Build();

app.UseCors(options => options
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowAnyOrigin());
app.UseSwagger();
app.UseSwaggerUI();
app.MapEndpoints();

app.Run();