using CloudGame.API.Handlers;
using CloudGame.Application.Extensions;
using CloudGame.Application.Handlers.UserHandler.Create;
using CloudGame.Application.Settings;
using CloudGame.Domain.Commom;
using CloudGame.Domain.Interfaces.Security;
using CloudGame.Infrastructure.EntityFramework;
using CloudGame.Infrastructure.EntityFramework.Seeder;
using CloudGame.Infrastructure.Extensions;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Serilog;
using Serilog.Formatting.Json;
using System.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserCommandValidator>();

builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .SelectMany(s => s.Value.Errors
            .Select(v => new Error(s.Key, v.ErrorMessage)))
            .ToList();

        return new BadRequestObjectResult(Result.Failure(errors));
    };
});

builder.Services.AddOpenApi();

builder.Services
    .AddApplicationHandlers()
    .AddInfrastructureServices(builder.Configuration);

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


builder.Services.AddExceptionHandler<CloudGameExceptionHandler>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TC - Fiap Cloud Game API",
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

builder.Host.UseSerilog((ctx, configuration) =>
configuration.ReadFrom.Configuration(ctx.Configuration)
.Enrich.FromLogContext().WriteTo.Console(new JsonFormatter()));
    

var app = builder.Build();

app.UseSerilogRequestLogging();

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
