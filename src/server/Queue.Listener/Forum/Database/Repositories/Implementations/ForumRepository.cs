using Microsoft.EntityFrameworkCore;
using Queue.Listener.Forum.Database.Repositories.Abstractions;

namespace Queue.Listener.Forum.Database.Repositories.Implementations;

public sealed class ForumRepository : IForumRepository
{
    private readonly ForumDbContext _context;

    public ForumRepository(ForumDbContext forumDbContext)
    {
        _context = forumDbContext;
    }

    public async Task SaveMessageAsync(string userName, string text)
    {
        await _context.Database.ExecuteSqlInterpolatedAsync(
            $@"
                INSERT INTO ""Users"" (""Name"")
                VALUES ({userName}) ON CONFLICT DO NOTHING;

                INSERT INTO ""Messages""(""UserId"", ""Text"")
                SELECT ""Id"", ({text}) 
                FROM ""Users"" 
                WHERE ""Name"" = ({userName});
            ");
    }
}