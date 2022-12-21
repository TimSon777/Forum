var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddCors();
services.AddEndpointsApiExplorer();
services.AddControllers();
services.AddSwaggerGen();
services.AddJwtProvider(configuration);

var app = builder.Build();

app.UseCors(options => options
    .AllowCredentials()
    .AllowAnyHeader()
    .AllowAnyMethod()
    .WithOrigins(configuration.GetString("ORIGIN:FRONT")));

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();