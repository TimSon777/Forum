using Application.Abstractions;
using Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Implementations;

public sealed class ForumRepository : IForumRepository
{
    private readonly ForumDbContext _context;

    public ForumRepository(ForumDbContext forumDbContext)
    {
        _context = forumDbContext;
    }

    public async Task SaveMessageAsync(SaveMessageItem message)
    {
        await _context.Database.ExecuteSqlInterpolatedAsync(
            $@"
                INSERT INTO ""Users"" (""Name"")
                VALUES ({message.UserName}) ON CONFLICT DO NOTHING;

                INSERT INTO ""Messages""(""UserId"", ""Text"", ""FileKey"")
                SELECT ""Id"", ({message.Text}), ({message.FileId})
                FROM ""Users"" 
                WHERE ""Name"" = ({message.UserName});
            ");
    }
}