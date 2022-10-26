// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder;

public static class MiddlewareConfiguration
{
    public static WebApplication Configure(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.MapControllers();
        return app;
    }
}