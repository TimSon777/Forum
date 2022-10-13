using Chat.API.Mapping;
using Chat.DAL.Abstractions.Chat;
using Chat.Infrastructure.MessageHandlers;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

var logger = new LoggerFactory().CreateLogger<Program>();

services.AddDatabase(configuration);
services.AddRepositories();
services.AddMessageHandlers(configuration);
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

        if (messages is null)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            return;
        }
        
        var items = messages.Select(x => x.ToSendMessageItem());
        await context.Response.WriteAsJsonAsync(items);
    });

app.UseRouting();
app.MapHub<MessageHub>("/forum");

app.Run();