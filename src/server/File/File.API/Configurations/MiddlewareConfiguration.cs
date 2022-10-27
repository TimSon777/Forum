// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder;

public static class MiddlewareConfiguration
{
    public static WebApplication Configure(this WebApplication app)
    {
        app.UseCors(options => options
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowAnyOrigin());
        app.UseSwagger();
        app.UseSwaggerUI();
        app.MapControllers();
        return app;
    }
}