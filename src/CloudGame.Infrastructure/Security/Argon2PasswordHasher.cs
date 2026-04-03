using CloudGame.Domain.Interfaces.Security;
using Isopoh.Cryptography.Argon2;

namespace CloudGame.Infrastructure.Security;

public sealed class Argon2PasswordHasher : IPasswordHasher
{
    public string CreateHash(string password)
    {
        return Argon2.Hash(password);
    }

    public bool VerifyPassword(string passwordHash, string password)
    {
        return Argon2.Verify(passwordHash, password);
    }
}
