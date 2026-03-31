using CloudGame.Domain.Entities;

namespace CloudGame.Domain.Contracts
{
    public interface IUserReadOnlyRepository : IReadOnlyRepository<User, int>
    {
        Task<IEnumerable<User>> FindAsync();
    }
}
