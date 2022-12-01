using Microsoft.EntityFrameworkCore;

namespace Forum.Queue.Listener;

public sealed class ForumDbContext : DbContext
{
    public ForumDbContext(DbContextOptions<ForumDbContext> options) 
        : base(options) 
    { }
}