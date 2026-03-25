using CloudGame.Domain.Entities;

namespace CloudGame.Domain.Contracts;

public interface IWriteOnlyRepository<in TEntity, in TId> where TEntity : Entity<TId>
{
    Task AddAsync(TEntity entity);

    Task UpdateAsync(TEntity entity);

    Task RemoveAsync(TId id);
}