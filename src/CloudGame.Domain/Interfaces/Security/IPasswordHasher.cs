namespace CloudGame.Domain.Interfaces.Security;

public interface IPasswordHasher
{
    string CreateHash(string password);

    bool VerifyPassword(string passwordHash, string password);
}
