using System.Reflection;
using EntityFrameworkCore.Triggers;
using LiWiMus.Core.Albums;
using LiWiMus.Core.Artists;
using LiWiMus.Core.Genres;
using LiWiMus.Core.Offices;
using LiWiMus.Core.OnlineConsultants;
using LiWiMus.Core.Plans;
using LiWiMus.Core.Playlists;
using LiWiMus.Core.Tracks;
using LiWiMus.Core.Transactions;
using LiWiMus.Core.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LiWiMus.Infrastructure.Data;

public class ApplicationContext : IdentityUserContext<User, int>
{
    private readonly IServiceProvider _serviceProvider;
    public DbSet<Album> Albums => Set<Album>();
    public DbSet<Artist> Artists => Set<Artist>();
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<Playlist> Playlists => Set<Playlist>();
    public DbSet<Track> Tracks => Set<Track>();
    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<OnlineConsultant> OnlineConsultants => Set<OnlineConsultant>();
    public DbSet<Plan> Plans => Set<Plan>();
    public DbSet<Office> Offices => Set<Office>();

    public ApplicationContext(DbContextOptions<ApplicationContext> options, IServiceProvider serviceProvider) : base(options)
    {
        _serviceProvider = serviceProvider;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override int SaveChanges()
    {
        return this.SaveChangesWithTriggers(base.SaveChanges, _serviceProvider, true);
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        return this.SaveChangesWithTriggers(base.SaveChanges, _serviceProvider, acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return this.SaveChangesWithTriggersAsync(base.SaveChangesAsync, _serviceProvider, true, cancellationToken);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
                                               CancellationToken cancellationToken = default)
    {
        return this.SaveChangesWithTriggersAsync(base.SaveChangesAsync, _serviceProvider, acceptAllChangesOnSuccess, cancellationToken);
    }
}