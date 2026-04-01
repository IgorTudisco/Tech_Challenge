using CloudGame.Domain.Entities;

namespace CloudGame.Application.Interfaces.Security
{
    public interface ITokenService
    {
        string GetToken(User user);
    }
}
