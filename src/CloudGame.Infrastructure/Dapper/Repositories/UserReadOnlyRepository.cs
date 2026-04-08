using CloudGame.Domain.Entities;
using CloudGame.Domain.Interfaces;
using CloudGame.Infrastructure.Dapper.Contracts;
using Dapper;
using System.Data;

namespace CloudGame.Infrastructure.Dapper.Repositories;

public sealed class UserReadOnlyRepository : AbstractRepository<User, int>, IUserReadOnlyRepository
{
    public UserReadOnlyRepository(IDapperContext context) : base(context)
    {
    }

    public Task<IEnumerable<User>> FindAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        using IDbConnection connection = Context.OpenConnection();
        return await connection.QueryFirstOrDefaultAsync<User>("SELECT Id,Name,Email,Password,Active,IsAdmin FROM [User] WHERE Email = @email", new { email });
    }
}
