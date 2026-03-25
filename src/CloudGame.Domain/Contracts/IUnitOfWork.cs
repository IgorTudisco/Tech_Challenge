using System.Data;

namespace CloudGame.Domain.Contracts;

public interface IUnitOfWork
{
    Task<IDbTransaction> BeginTransationAsync(IsolationLevel isolationLevel);

    Task<IDbTransaction> BeginTransationAsync();

    Task SaveChangesAsync();
}