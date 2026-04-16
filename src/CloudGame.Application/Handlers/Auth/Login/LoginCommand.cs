using CloudGame.Domain.Handlers;

namespace CloudGame.Application.Handlers.Auth.Login;

public class LoginCommand : ICommand
{
    public string User { get; set; }
    public string Password { get; set; }
}
