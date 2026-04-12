using CloudGame.Application.Handlers.Auth.Login;
using CloudGame.Application.Settings;
using CloudGame.Domain.Commom;
using CloudGame.Domain.Entities;
using CloudGame.Domain.Interfaces;
using CloudGame.Domain.Interfaces.Security;
using CloudGame.Domain.ValueObjects;
using Microsoft.Extensions.Options;
using Moq;
using System.Xml.Linq;

namespace CloudGame.Test.Handlers.Auth;

public class LoginHandlerTests
{
    private static readonly Error FailedLoginError = new("LoginInvalido", "Login ou senha invalida");

    // SUT = System Under Test
    private static LoginHandler MakeSut(
        IOptions<JwtSettings>? jwtSettingsOption = null,
        IUserReadOnlyRepository? userReadOnlyRepository = null,
        IPasswordHasher? passwordHasher = null)
    {
        if (jwtSettingsOption is null)
        {
            var mock = new Mock<IOptions<JwtSettings>>();
            mock.Setup(o => o.Value).Returns(new JwtSettings
            {
                EncriptKey = "EncriptKey",
                ExpiresInMinutes = 60,
                Audience = "Audience",
                Issuer = "Issuer",
            });

            jwtSettingsOption = mock.Object;
        }

        if (userReadOnlyRepository is null)
        {
            var mock = new Mock<IUserReadOnlyRepository>();
            userReadOnlyRepository = mock.Object;
        }

        if (passwordHasher is null)
        {
            var mock = new Mock<IPasswordHasher>();
            passwordHasher = mock.Object;
        }

        return new LoginHandler(jwtSettingsOption, userReadOnlyRepository, passwordHasher);
    }


    [Fact]
    public async Task ShouldReturnInvalidLoginWhenUserNotFound_Test()
    {
        var command = new LoginCommand { User = "invalid_user" };

        var mock = new Mock<IUserReadOnlyRepository>();

        mock.Setup(x => x.GetByEmailAsync(command.User)).ReturnsAsync(null as User);

        var sut = MakeSut(userReadOnlyRepository: mock.Object);

        var result = await sut.HandleAsync(command, CancellationToken.None);

        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Single(result.Errors);
        Assert.Equal(FailedLoginError.Code, result.Errors.Single().Code);
        Assert.Equal(FailedLoginError.Description, result.Errors.Single().Description);
    }

    [Fact]
    public async Task ShouldReturnInvalidLoginWhenUserIsNotActive_Test()
    {
        var command = new LoginCommand { User = "invalid_user" };

        var mock = new Mock<IUserReadOnlyRepository>();

        User userMockResponse = new("MockedUser", "MockedEmail", "MockedPassword", DateTime.Now, false);
        userMockResponse.SetActive(false);
        mock.Setup(x => x.GetByEmailAsync(command.User)).ReturnsAsync(null as User);

        var sut = MakeSut(userReadOnlyRepository: mock.Object);

        var result = await sut.HandleAsync(command, CancellationToken.None);

        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Single(result.Errors);
        Assert.Equal(FailedLoginError.Code, result.Errors.Single().Code);
        Assert.Equal(FailedLoginError.Description, result.Errors.Single().Description);
    }
}
