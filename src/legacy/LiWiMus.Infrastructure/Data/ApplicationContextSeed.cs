using LiWiMus.Infrastructure.Data.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LiWiMus.Infrastructure.Data;

public static class ApplicationContextSeed
{
    public static async Task SeedAsync(IServiceProvider provider, 
                                       EnvironmentType environmentType,
                                       ILogger logger, 
                                       int retry = 0)
    {
        try
        {
            var applicationContext = provider.GetRequiredService<ApplicationContext>();
            logger.LogInformation("Seeding Database...");
            var seeders = provider.GetServices<ISeeder>();


            if (applicationContext.Database.IsMySql())
            {
                await applicationContext.Database.MigrateAsync();
            }

            foreach (var seeder in seeders.OrderByDescending(x => x.Priority))
            {
                await seeder.SeedAsync(environmentType);
            }

            await applicationContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            // if (Random.Shared.TryProbability(5))
            // {
            //     File.WriteAllText(@"C:\Users\Тимур\Desktop\LiWiMus\src\LiWiMus.Web.API/EXCEPTION" + Guid.NewGuid().ToString()[..10] + ".txt", ex.Message + "\n" + ex.StackTrace + "\n" + ex.GetType());
            // }
            
            if (retry >= 10)
            {
                throw;
            }

            retry++;

            logger.LogError("Error while seeding database: {Message}", ex.Message);
            await SeedAsync(provider, environmentType, logger, retry);
            throw;
        }
    }
}