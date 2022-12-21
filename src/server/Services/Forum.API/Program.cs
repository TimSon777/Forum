using FluentValidation;
using Forum.API;
using Forum.API.Features.Messages.History;
using MinimalApi.Endpoint.Extensions;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

services.AddAuth(configuration);
services.AddEndpoints();
services.AddSwaggerGen();
services.AddForumDatabase(configuration);
services.AddMessageRepository();
services.AddEndpointsApiExplorer();
services.AddSignalR();
services.AddCors();
services.AddCache(configuration);
services.AddCachingService();
services.AddRabbitMq(configuration,
    configure => configure.AddConsumer<NotificationFileUploadedConsumer>());
services.AddValidatorsFromAssemblyContaining<Validator>();
var app = builder.Build();

app.UseCors(options => options
    .AllowCredentials()
    .AllowAnyHeader()
    .AllowAnyMethod()
    .WithOrigins(configuration.GetString("ORIGIN:FRONT")));

app.UseSwagger();
app.UseSwaggerUI();
app.UseRouting();
app.MapHub<SupportChat>("/forum");
app.MapEndpoints();

app.Run();