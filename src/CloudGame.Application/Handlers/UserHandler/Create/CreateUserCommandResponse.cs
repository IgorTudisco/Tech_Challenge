using CloudGame.Domain.Handlers;

namespace CloudGame.Application.Handlers.UserHandler.Create;

public record CreateUserCommandResponse(int Id, string Nome, string Email) : IResponse;
