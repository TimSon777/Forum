using Chat.DAL.Abstractions.Chat;
using Chat.Infrastructure.MessageHandlers;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

var logger = new LoggerFactory().CreateLogger<Program>();

services.AddDatabase(configuration, logger);
//services.AddMessageConsumer(configuration);
services.AddRepositories();
services.AddMessageHandlers();
services.AddSignalR();
services.AddCors();
services.AddFluentValidators();
var app = builder.Build();

app.UseCors(options =>
{
    var frontOrigin = configuration["ORIGIN:FRONT"] 
                      ?? throw new AggregateException(); 
    
    options
        .AllowCredentials()
        .AllowAnyHeader()
        .AllowAnyMethod()
        .WithOrigins(frontOrigin);
});

app.MapGet("/history/{count}",
    async (HttpContext context, IChatRepository chatRepository, int count) =>
    {
        var messages = await chatRepository.GetMessagesAsync(count);
        await context.Response.WriteAsJsonAsync(messages);
    });

app.UseRouting();
app.MapHub<MessageHub>("/forum");

app.Run();