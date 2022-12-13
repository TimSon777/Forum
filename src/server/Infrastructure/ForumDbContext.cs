using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public sealed class ForumDbContext : DbContext
{
    public ForumDbContext(DbContextOptions<ForumDbContext> options) 
        : base(options) 
    { }
}