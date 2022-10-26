using Microsoft.EntityFrameworkCore;
using Queue.Listener.Forum.Database.Entities;

namespace Queue.Listener.Forum.Database;

public sealed class ForumDbContext : DbContext
{
    public ForumDbContext(DbContextOptions<ForumDbContext> options) 
        : base(options) 
    { }
}