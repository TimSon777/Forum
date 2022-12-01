using Forum.Infrastructure.MessageHandlers;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

services.AddSwaggerGen();
services.AddEndpointsApiExplorer();

services.AddControllers();
services.AddDatabase(configuration);
services.AddRepositories();
services.AddMassTransitPublisher(configuration);
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

app.UseSwaggerUI();
app.UseRouting();
app.MapHub<MessageHub>("/forum");
app.MapControllers();

app.Run();