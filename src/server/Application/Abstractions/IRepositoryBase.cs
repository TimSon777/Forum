using SharedKernel.Data;

namespace Application.Abstractions;

public interface IRepositoryBase<TEntity>
    where TEntity : BaseEntity
{
    void Add(TEntity entity);
    Task CommitAsync();
}