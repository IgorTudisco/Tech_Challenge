using CloudGame.Domain.Entities;

namespace CloudGame.Domain.Contracts
{
    public interface IReadOnlyRepository<TEntity, in TId> where TEntity : Entity<TId>
    {
        Task<TEntity> GetByIdAsync(TId id);
    }
}
