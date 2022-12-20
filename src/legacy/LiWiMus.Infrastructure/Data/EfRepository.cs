using Ardalis.Specification.EntityFrameworkCore;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Infrastructure.Data;

public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
    public EfRepository(ApplicationContext dbContext) : base(dbContext)
    {
    }
}