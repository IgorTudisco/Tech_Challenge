using CloudGame.Domain.Commom;
using CloudGame.Domain.Entities;
using CloudGame.Domain.Interfaces;
using Moq;

namespace CloudGame.Test.Handlers.UserHandler;

public class DeleteUserHandlerTest
{
    private static readonly Error FailedDeleteError = new("NotFound", "Usuário não encontrado");

    // SUT = System Under Test
    private static DeleteUserHandler MakeSut(
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

        return new DeleteUserHandler(userWriteOnlyRepository, userReadOnlyRepository, unitOfWork);
    }

    [Fact]
    public async Task ShouldBeInvalidWhenUserIsNotFound_Test()
    {
        var command = new DeleteUserCommand { Id = 1 };

        var userReadonlyRepositoryMock = new Mock<IUserReadOnlyRepository>();
        userReadonlyRepositoryMock.Setup(x => x.GetByIdAsync(command.Id)).ReturnsAsync(null as User);

        var userWriteRepositoryMock = new Mock<IUserWriteOnlyRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        var sut = MakeSut(userReadOnlyRepository: userReadonlyRepositoryMock.Object,
                    userWriteOnlyRepository: userWriteRepositoryMock.Object,
                    unitOfWork: unitOfWorkMock.Object);

        var result = await sut.HandleAsync(command, CancellationToken.None);

        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Single(result.Errors);
        Assert.Equal(FailedDeleteError.Code, result.Errors.Single().Code);
        Assert.Equal(FailedDeleteError.Description, result.Errors.Single().Description);

        userReadonlyRepositoryMock.Verify(x => x.GetByIdAsync(command.Id), Times.Once);
        userWriteRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<User>()), Times.Never);
        unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Never);
    }
}
