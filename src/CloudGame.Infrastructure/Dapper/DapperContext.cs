using CloudGame.Infrastructure.Dapper.Contracts;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CloudGame.Infrastructure.Dapper
{
    public class DapperContext : IDapperContext
    {
        private readonly string _connectionString;

        public DapperContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection OpenConnection() => new SqlConnection(_connectionString);
    }
}
