using CloudGame.Domain.Handlers;

namespace CloudGame.Application.Handlers.Auth.Login;

public class LoginResponse : IResponse
{
    public string Token { get; set; }
    public DateTime ExpirationDate { get; set; }
}
