using System.Data;

namespace CloudGame.Infrastructure.Dapper.Contracts
{
    public interface IDapperContext
    {
        IDbConnection OpenConnection();
    }
}
