var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

services.AddSwagger();
services.AddControllers();
services.AddAws(configuration);
services.AddFileProvider();
services.AddBucketCreator();
services.AddCors();
services.AddFileMetadataPublisher(configuration);

var app =builder.Build();

app.UseCors(options => options
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowAnyOrigin());
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();