using CloudGame.Domain.Contracts;
using CloudGame.Domain.Entities;

namespace CloudGame.Infrastructure.EntityFramework.Repositories
{
    public sealed class UserRepository : AbstractRepository<User, int>, IUserWriteOnlyRepository
    {
        public UserRepository(AppDbContext dbContext) : base(dbContext)
        { }
    }
}
