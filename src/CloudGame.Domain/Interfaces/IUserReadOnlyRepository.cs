using CloudGame.Domain.Commom;
using CloudGame.Domain.Entities;
using CloudGame.Domain.Parameters;

namespace CloudGame.Domain.Interfaces
{
    public interface IUserReadOnlyRepository : IReadOnlyRepository<User, int>
    {
        Task<Pagination<User>> FindAsync(FindUsersParameter parameters);

        Task<User?> GetByEmailAsync(string email);
    }
}
