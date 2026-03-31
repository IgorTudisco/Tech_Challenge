using CloudGame.Domain.Entities;
using CloudGame.Domain.Interfaces;

namespace CloudGame.Infrastructure.EntityFramework.Repositories
{
    public sealed class UserRepository: AbstractRepository<User, int>, IUserWriteOnlyRepository
    {
        public UserRepository(AppDbContext dbContext) : base(dbContext)
        { }
    }
}
