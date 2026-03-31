using CloudGame.Domain.Entities;
using CloudGame.Domain.Interfaces;
using CloudGame.Infrastructure.Dapper.Contracts;

namespace CloudGame.Infrastructure.Dapper.Repositories
{
    public sealed class UserRepository : AbstractRepository<User, int>, IUserReadOnlyRepository
    {
        public UserRepository(IDapperContext context) : base(context)
        { }

        public Task<IEnumerable<User>> FindAsync()
        {
            throw new NotImplementedException();
        }
    }
}
