using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public sealed class ForumDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Message> Messages => Set<Message>();

    public ForumDbContext(DbContextOptions<ForumDbContext> options) 
        : base(options) 
    { }
}