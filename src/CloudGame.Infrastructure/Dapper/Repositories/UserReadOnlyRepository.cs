using CloudGame.Domain.Commom;
using CloudGame.Domain.Entities;
using CloudGame.Domain.Interfaces;
using CloudGame.Domain.Parameters;
using CloudGame.Domain.ValueObjects;
using CloudGame.Infrastructure.Dapper.Contracts;
using Dapper;
using System.Data;

namespace CloudGame.Infrastructure.Dapper.Repositories;

public sealed class UserReadOnlyRepository(IDapperContext context)
    : AbstractRepository<User, int>(context), IUserReadOnlyRepository
{
    public async Task<Pagination<User>> FindAsync(FindUsersParameter parameters)
    {
        var sqlBuilder = new SqlBuilder();

        if (parameters.Active.HasValue)
            sqlBuilder.Where("Active=@active", new { active = parameters.Active });

        if (parameters.IsAdmin.HasValue)
            sqlBuilder.Where("IsAdmin=@isAdmin", new { isAdmin = parameters.IsAdmin });

        if (!string.IsNullOrWhiteSpace(parameters.Name))
            sqlBuilder.Where("Name=@name", new { name = $"%{parameters.Name}%" });

        var countQuery = sqlBuilder.AddTemplate("Select count(*) from [User] /**where**/");
        var queryUsers = sqlBuilder.AddTemplate(@"select Id, Name, Email, Active, IsAdmin from [User]
                                                /**where**/ ORDER BY Id OFFSET @skip ROWS FETCH NEXT @size ROWS ONLY",
                                                new { skip = parameters.Skip, size = parameters.PageSize });

        using IDbConnection connection = Context.OpenConnection();

        int count = await connection.ExecuteScalarAsync<int>(countQuery.RawSql, countQuery.Parameters);
        var users = (await connection.QueryAsync<User>(queryUsers.RawSql, queryUsers.Parameters)).ToList();

        return new Pagination<User>(users, count);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        using IDbConnection connection = Context.OpenConnection();
        return await connection.QueryFirstOrDefaultAsync<User>("SELECT Id,Name,Email,Password,Active,IsAdmin FROM [User] WHERE Email = @email", new { email });
    }

    public override async Task<User> GetByIdAsync(int id)
    {
        using IDbConnection connection = Context.OpenConnection();
        return await connection.QueryFirstOrDefaultAsync<User>("SELECT Id,Name,Email,Active,IsAdmin,BirthDate,CreatedAt,UpdateAt FROM [User] WHERE Id = @id", new { id });
    }
}
