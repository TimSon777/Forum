using EntityFrameworkCore.Triggers;
using LiWiMus.Core.Interfaces;
using LiWiMus.Core.Plans.Interfaces;
using LiWiMus.Core.Roles;
using LiWiMus.Core.Roles.Interfaces;
using LiWiMus.Core.Transactions;
using LiWiMus.Core.Users;
using LiWiMus.SharedKernel;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Infrastructure.Data.Config;

public static class TriggersConfiguration
{
    private static volatile bool _forTests;
    private static readonly object Lock = new();
    
    public static void ConfigureTriggers()
    {
        if (_forTests)
        {
            return;
        }
        
        lock (Lock)
        {
            if (_forTests)
            {
                return;
            }
            
            AddTriggers();
            _forTests = true;
        }
    }

    private static void AddTriggers()
    {
        Triggers<BaseEntity>.Inserting += entry => entry.Entity.CreatedAt = entry.Entity.ModifiedAt = DateTime.UtcNow;
        Triggers<BaseEntity>.Updating += entry => entry.Entity.ModifiedAt = DateTime.UtcNow;

        Triggers<BaseUserEntity>.Inserting += entry => entry.Entity.CreatedAt = entry.Entity.ModifiedAt = DateTime.UtcNow;
        Triggers<BaseUserEntity>.Updating += entry => entry.Entity.ModifiedAt = DateTime.UtcNow;

        Triggers<Transaction>.GlobalInserting.Add<IRepository<User>>(async entry =>
        {
            var transaction = entry.Entity;
            var userId = transaction.UserId;
            var user = await entry.Service.GetByIdAsync(userId);
            if (user != null)
            {
                user.Balance += transaction.Amount;
            }
        });

        Triggers<User, ApplicationContext>.Inserted += entry => entry.Context.Transactions.Add(new Transaction
        {
            User = entry.Entity,
            Amount = 100,
            Description = "Gift for registration"
        });

        Triggers<User>.GlobalInserted.Add<IAvatarService>(async entry =>
            await entry.Service.SetRandomAvatarAsync(entry.Entity));

        Triggers<User>.GlobalInserted.Add<IUserPlanManager>(async entry =>
            await entry.Service.AddToDefaultPlanAsync(entry.Entity));

        Triggers<User>.GlobalInserted.Add<IRoleManager>(async entry =>
            await entry.Service.AddToRoleAsync(entry.Entity, DefaultRoles.User.Name));
    }
}