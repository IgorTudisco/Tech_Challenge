using CloudGame.Domain.Entities;

namespace CloudGame.Domain.Interfaces
{   
    public interface IUserReadOnlyRepository : IReadOnlyRepository<User, int>
    {
        Task<IEnumerable<User>> FindAsync();
    }
}
