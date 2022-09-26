using Chat.DAL.Abstractions;
using Chat.DAL.Abstractions.Chat;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

services.AddMessageConsumer(configuration, 10);
services.AddDatabase(configuration);
services.AddRepositories();

var app = builder.Build();

app.MapGet("/history/{count}",
    async (HttpContext context, IChatRepository chatRepository, int count) =>
    {
        var messages = await chatRepository.GetMessagesAsync(count);
        await context.Response.WriteAsJsonAsync(messages);
    });

app.Run();