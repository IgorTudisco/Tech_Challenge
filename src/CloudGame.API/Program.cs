using CloudGame.Application.Handlers.Auth.Login;
using CloudGame.Application.Handlers.UserHandler.ChangeActive;
using CloudGame.Application.Handlers.UserHandler.Create;
using CloudGame.Application.Handlers.UserHandler.Update;
using CloudGame.Application.Settings;
using CloudGame.Domain.Handlers;
using CloudGame.Domain.Interfaces;
using CloudGame.Domain.Interfaces.Security;
using CloudGame.Infrastructure.Dapper;
using CloudGame.Infrastructure.Dapper.Contracts;
using CloudGame.Infrastructure.Dapper.Repositories;
using CloudGame.Infrastructure.EntityFramework;
using CloudGame.Infrastructure.EntityFramework.Repositories;
using CloudGame.Infrastructure.EntityFramework.Seeder;
using CloudGame.Infrastructure.Security;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidatorsFromAssemblyContaining<CreateUserCommandValidator>();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddScoped<IHandler<LoginCommand, LoginResponse>, LoginHandler>();
var jwtSettingsSection = builder.Configuration.GetRequiredSection("JwtSettings");
builder.Services.Configure<JwtSettings>(jwtSettingsSection);

var encriptKey = jwtSettingsSection.GetValue<string>("EncriptKey")!;
var key = Encoding.ASCII.GetBytes(encriptKey);
builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer
    (
        builder.Configuration.GetConnectionString("Default")
    )
);

builder.Services.AddScoped<IDbConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<IDapperContext>(sp => new DapperContext(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<IUserReadOnlyRepository, UserReadOnlyRepository>();
builder.Services.AddScoped<IUserWriteOnlyRepository, UserWriteOnlyRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(sp => new UnitOfWork(sp.GetRequiredService<AppDbContext>()));


builder.Services.AddScoped<IPasswordHasher, Argon2PasswordHasher>();

builder.Services.AddScoped<IHandler<LoginCommand, LoginResponse>, LoginHandler>();
builder.Services.AddScoped<IHandler<CreateUserCommand, CreateUserCommandResponse>, CreateUserCommandHandler>();
builder.Services.AddScoped<IHandler<UpdateUserCommand, UpdateUserResponse>, UpdateUserHandler>();
builder.Services.AddScoped<IHandler<ChangeActiveUserCommand, ChangeActiveUserResponse>, ChangeActiveUserHandler>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1"
    });

    options.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter JWT token like: Bearer {your token}",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });

    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("bearer", document)] = []
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();

    await DatabaseSeeder.SeedAsync(appDbContext, passwordHasher);
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
