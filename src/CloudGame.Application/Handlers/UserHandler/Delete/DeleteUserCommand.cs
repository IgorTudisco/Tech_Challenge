using CloudGame.Domain.Handlers;

namespace CloudGame.Application.Handlers.UserHandler.Delete;

public record DeleteUserCommand : ICommand
{
    public int Id { get; init; }
}
