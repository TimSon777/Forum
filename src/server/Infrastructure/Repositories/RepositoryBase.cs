using Application.Abstractions;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Data;

namespace Infrastructure.Repositories;

public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
    where TEntity : BaseEntity
{
    protected readonly ForumDbContext Context;
    protected readonly DbSet<TEntity> Set;

    public RepositoryBase(ForumDbContext context)
    {
        Context = context;
        Set = context.Set<TEntity>();
    }

    public void Add(TEntity entity)
    {
        Set.Add(entity);
    }

    public async Task CommitAsync()
    {
        await Context.SaveChangesAsync();
    }
}