using Application.Abstractions;
using Domain.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class MessageRepository : RepositoryBase<Message>, IMessageRepository
{
    public MessageRepository(ForumDbContext context) : base(context)
    { }

    public async Task<IEnumerable<Message>> GetMessagesAsync(string userName, int count)
    {
        return await Set
            .Where(m => m.UserFrom.Name == userName || m.UserTo != null && m.UserTo.Name == userName)
            .Include(m => m.UserFrom)
            .Include(m => m.UserTo)
            .OrderByDescending(message => message.Id)
            .Take(count)
            .ToListAsync();
    }

    public async Task SaveMessageAsync(SaveMessageItem saveMessageItem)
    {
        var userFrom = await Context.Users
            .Include(u => u.Mate)
            .FirstAsync(u => u.Name == saveMessageItem.UserName);

        var message = new Message
        {
            Text = saveMessageItem.Text,
            FileKey = saveMessageItem.FileId,
            UserFrom = userFrom,
            UserTo = userFrom.Mate
        };
        
        Set.Add(message);

        await CommitAsync();
    }
}