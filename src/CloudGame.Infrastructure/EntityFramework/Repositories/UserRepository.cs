using CloudGame.Domain.Entities;
using CloudGame.Domain.Interfaces;

namespace CloudGame.Infrastructure.EntityFramework.Repositories
{
    public sealed class UserRepository(AppDbContext dbContext) : AbstractRepository<User, int>(dbContext), IUserWriteOnlyRepository
    {
    }
}
