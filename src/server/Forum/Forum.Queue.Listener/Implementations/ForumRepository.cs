using Forum.Queue.Listener.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Forum.Queue.Listener.Implementations;

public sealed class ForumRepository : IForumRepository
{
    private readonly ForumDbContext _context;

    public ForumRepository(ForumDbContext forumDbContext)
    {
        _context = forumDbContext;
    }

    public async Task SaveMessageAsync(string userName, string text, string? fileKey)
    {
        await _context.Database.ExecuteSqlInterpolatedAsync(
            $@"
                INSERT INTO ""Users"" (""Name"")
                VALUES ({userName}) ON CONFLICT DO NOTHING;

                INSERT INTO ""Messages""(""UserId"", ""Text"", ""FileKey"")
                SELECT ""Id"", ({text}), ({fileKey})
                FROM ""Users"" 
                WHERE ""Name"" = ({userName});
            ");
    }
}