using CloudGame.Domain.Handlers;

namespace CloudGame.Application.Handlers.UserHandler.Create;

public sealed class CreateUserCommand : ICommand
{
    public string Name { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public DateTime BirthDate { get; set; }
}

