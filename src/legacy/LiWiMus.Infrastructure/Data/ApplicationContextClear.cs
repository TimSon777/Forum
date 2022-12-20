using LiWiMus.Core.Chats.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LiWiMus.Infrastructure.Data;

public static class ApplicationContextClear
{
    public static async Task ClearAsync(ApplicationContext applicationContext, ILogger logger, bool isDevelopment)
    {
        await DeleteAllOnlineConsultantsAsync(applicationContext, logger);
        
        if (isDevelopment)
        {
            await DeleteOpenedChatsAsync(applicationContext, logger);
        }
    }
    
    private static async Task DeleteAllOnlineConsultantsAsync(ApplicationContext applicationContext, ILogger logger)
    {
        var onlineConsultants = applicationContext.OnlineConsultants.ToList();
        applicationContext.OnlineConsultants.RemoveRange(onlineConsultants);
        await applicationContext.SaveChangesAsync();
        logger.LogInformation("Online consultants were removed");
    }

    private static async Task DeleteOpenedChatsAsync(ApplicationContext applicationContext, ILogger logger)
    {
        var chats = applicationContext.Users
            .Include(u => u.UserChats)
            .Select(u => u.UserChats)
            .SelectMany(c => c)
            .Where(c => c.Status == ChatStatus.Opened);
        
        if (chats.Any())
        {
            applicationContext.RemoveRange(chats);
            await applicationContext.SaveChangesAsync();
        }
        
        logger.LogInformation("Opened chats was deleted");
    }
}