var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddSwaggerGen();
services.AddEndpointsApiExplorer();

services.AddCache(configuration);
services.AddMetadataProvider();
services.AddCollectionCreatorBackgroundService();
services.AddFileMetadataPublisher(configuration);

var app = builder.Build();

app.UseSwaggerUI();
app.MapControllers();
app.Run();