using CloudGame.Application.Handlers.UserHandler.Update;
using CloudGame.Domain.Commom;
using CloudGame.Domain.Entities;
using CloudGame.Domain.Interfaces;
using Moq;

namespace CloudGame.Test.Handlers.UserHandler;

public class UpdateUserHandlerTest
{
    private static readonly Error FailedUpdateError = new("NotFound", "Usuário não encontrado");

    // SUT = System Under Test
    private static UpdateUserHandler MakeSut(
        IUserWriteOnlyRepository? userWriteOnlyRepository = null,
        IUserReadOnlyRepository? userReadOnlyRepository = null,
        IUnitOfWork? unitOfWork = null)
    {
        if (userWriteOnlyRepository is null)
        {
            var mock = new Mock<IUserWriteOnlyRepository>();
            userWriteOnlyRepository = mock.Object;
        }

        if (userReadOnlyRepository is null)
        {
            var mock = new Mock<IUserReadOnlyRepository>();
            userReadOnlyRepository = mock.Object;
        }

        if (unitOfWork is null)
        {
            var mock = new Mock<IUnitOfWork>();
            unitOfWork = mock.Object;
        }

        return new UpdateUserHandler(userWriteOnlyRepository, userReadOnlyRepository, unitOfWork);
    }

    [Fact]
    public async Task ShouldBeInvalidWhenUserIsNotFound_Test()
    {
        var command = new UpdateUserCommand { Id = 1 }; 

        var mock = new Mock<IUserReadOnlyRepository>();

        mock.Setup(x => x.GetByIdAsync(command.Id)).ReturnsAsync(null as User);

        var sut = MakeSut(userReadOnlyRepository: mock.Object);

        var result = await sut.HandleAsync(command, CancellationToken.None);

        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Single(result.Errors);
        Assert.Equal(FailedUpdateError.Code, result.Errors.Single().Code);
        Assert.Equal(FailedUpdateError.Description, result.Errors.Single().Description);
    }
}
