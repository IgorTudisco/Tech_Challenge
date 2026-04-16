using CloudGame.Domain.Interfaces;
using CloudGame.Domain.Interfaces.Security;
using CloudGame.Infrastructure.Dapper;
using CloudGame.Infrastructure.Dapper.Contracts;
using CloudGame.Infrastructure.Dapper.Repositories;
using CloudGame.Infrastructure.EntityFramework;
using CloudGame.Infrastructure.EntityFramework.Repositories;
using CloudGame.Infrastructure.Security;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace CloudGame.Infrastructure.Extensions
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer
                (
                    configuration.GetConnectionString("Default")
                )
            );

            services.AddScoped<IDbConnection>(sp => new SqlConnection(configuration.GetConnectionString("Default")));
            services.AddScoped<IDapperContext>(sp => new DapperContext(configuration));
            services.AddScoped<IUserWriteOnlyRepository, UserWriteOnlyRepository>();
            services.AddScoped<IUserReadOnlyRepository, UserReadOnlyRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>(sp => new UnitOfWork(sp.GetRequiredService<AppDbContext>()));
            services.AddScoped<IPasswordHasher, Argon2PasswordHasher>();

            return services;
        }
    }
}
