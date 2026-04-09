using CloudGame.Domain.Handlers;

namespace CloudGame.Application.Handlers.UserHandler.ChangeActive;

public class ChangeActiveUserCommand : ICommand
{
    public int Id { get; set; }
}
