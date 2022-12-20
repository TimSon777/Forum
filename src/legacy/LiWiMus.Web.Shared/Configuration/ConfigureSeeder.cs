using LiWiMus.Infrastructure;
using LiWiMus.Infrastructure.Data;
using LiWiMus.Infrastructure.Data.Seeders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LiWiMus.Web.Shared.Configuration;

public static class ConfigureSeeder
{
    public static void AddSeeders(this IServiceCollection services)
    {
        var seederInterface = typeof(ISeeder);
        var seeders = seederInterface
            .Assembly
            .GetTypes()
            .Where(t => t.IsAssignableTo(seederInterface) && !t.IsInterface);
        
        foreach (var seeder in seeders)
        {
            services.AddScoped(seederInterface, seeder);
        }
    }

    public static async Task UseSeedersAsync(IServiceProvider serviceProvider, ILogger logger, IWebHostEnvironment environment)
    {
        try
        {
            using var scope = serviceProvider.CreateScope();
            var envType = Enum.Parse<EnvironmentType>(environment.EnvironmentName);
            await ApplicationContextSeed.SeedAsync(scope.ServiceProvider, envType, logger);
            //await ApplicationContextClear.ClearAsync(applicationContext, logger, environment.IsDevelopment());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred seeding the DB");
        }
    }
}