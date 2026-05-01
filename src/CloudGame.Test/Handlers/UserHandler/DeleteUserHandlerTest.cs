using CloudGame.Application.Handlers.UserHandler.Delete;
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

    [Fact]
    public async Task ShouldBeDeletedAndUserDataMustBeCleared_Test()
    {
        var command = new DeleteUserCommand { Id = 1 };

        var userReadonlyRepositoryMock = new Mock<IUserReadOnlyRepository>();

        var user = new User("John Doe", "john.doe@example", "password123", new DateTime(1990, 1, 1), false);

        userReadonlyRepositoryMock.Setup(x => x.GetByIdAsync(command.Id)).ReturnsAsync(user);

        var userWriteRepositoryMock = new Mock<IUserWriteOnlyRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        var sut = MakeSut(userReadOnlyRepository: userReadonlyRepositoryMock.Object,
                    userWriteOnlyRepository: userWriteRepositoryMock.Object,
                    unitOfWork: unitOfWorkMock.Object);

        var result = await sut.HandleAsync(command, CancellationToken.None);

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.Empty(result.Errors);
        Assert.Equal("@deleted", user.Name);
        Assert.Equal("@deleted", user.Password);
        Assert.False(user.Active);
        Assert.Equal(new DateTime(1950, 1, 1), user.BirthDate);

        userReadonlyRepositoryMock.Verify(x => x.GetByIdAsync(command.Id), Times.Once);
        userWriteRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<User>()), Times.Once);
        unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }
}
