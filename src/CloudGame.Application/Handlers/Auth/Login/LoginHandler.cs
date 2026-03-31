using CloudGame.Application.Settings;
using CloudGame.Domain.Handlers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CloudGame.Application.Handlers.Auth.Login;

public class LoginHandler(IOptions<JwtSettings> jwtSettingsOption) : IHandler<LoginCommand, LoginResponse>
{
    public async Task<LoginResponse> HandleAsync(LoginCommand command, CancellationToken cancellationToken)
    {
        Claim[] claims = [
             new(ClaimTypes.Name, "nome do usuario"),
             new(ClaimTypes.Role, "admin"),
        ];

        JwtSettings jwtSettings = jwtSettingsOption.Value;
        var key = Encoding.ASCII.GetBytes(jwtSettings.EncriptKey);
        var expirationDate = DateTime.UtcNow.AddMinutes(jwtSettings.ExpiresInMinutes);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expirationDate,
            SigningCredentials =
              new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature),
            Audience = jwtSettings.Audience,
            Issuer = jwtSettings.Issuer,
        };

        JwtSecurityTokenHandler tokenHandler = new();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        return new()
        {
            Token = tokenHandler.WriteToken(securityToken),
            ExpirationDate = expirationDate,
        };
    }
}
