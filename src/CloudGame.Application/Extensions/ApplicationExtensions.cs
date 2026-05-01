using CloudGame.Application.Handlers.Auth.Login;
using CloudGame.Application.Handlers.UserHandler.ChangeActive;
using CloudGame.Application.Handlers.UserHandler.Create;
using CloudGame.Application.Handlers.UserHandler.Delete;
using CloudGame.Application.Handlers.UserHandler.Find;
using CloudGame.Application.Handlers.UserHandler.GetById;
using CloudGame.Application.Handlers.UserHandler.Update;
using CloudGame.Domain.Commom;
using CloudGame.Domain.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace CloudGame.Application.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplicationHandlers(this IServiceCollection services)
    {
        services.AddScoped<IHandler<LoginCommand, LoginResponse>, LoginHandler>();
        services.AddScoped<IHandler<CreateUserCommand, CreateUserCommandResponse>, CreateUserCommandHandler>();
        services.AddScoped<IHandler<UpdateUserCommand, UpdateUserResponse>, UpdateUserHandler>();
        services.AddScoped<IHandler<ChangeActiveUserCommand>, ChangeActiveUserHandler>();
        services.AddScoped<IHandler<DeleteUserCommand>, DeleteUserHandler>();

        services.AddScoped<IHandler<GetUserByIdQuery, GetUserByIdResponse>, GetUserByIdQueryHandler>();
        services.AddScoped<IHandler<FindUsersQuery, Pagination<FindUsersQueryResponse>>, FindUsersQueryHandler>();

        return services;
    }
}
