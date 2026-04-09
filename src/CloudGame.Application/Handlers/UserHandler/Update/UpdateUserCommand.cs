using CloudGame.Domain.Handlers;

namespace CloudGame.Application.Handlers.UserHandler.Update;

public sealed class UpdateUserCommand : ICommand
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public DateTime BirthDate { get; set; }
}
