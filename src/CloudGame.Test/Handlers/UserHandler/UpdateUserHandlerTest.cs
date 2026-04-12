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
        Assert.Equal(FailedUpdateError.Code, result.Errors.Single().Code);
        Assert.Equal(FailedUpdateError.Description, result.Errors.Single().Description);

        userReadonlyRepositoryMock.Verify(x => x.GetByIdAsync(command.Id), Times.Once);
        userWriteRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<User>()), Times.Never);
        unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Never);
    }

    [Fact]
    public async Task ShouldUpdateSuccessfully_Test()
    {
        var command = new UpdateUserCommand
        {
            Id = 1,
            BirthDate = DateTime.Now,
            Email = "validemail@test.com",
            Name = "Awesome Person"
        };


        var userReadonlyRepositoryMock = new Mock<IUserReadOnlyRepository>();
        User user = new("oldName", "oldemail@teste.com", "topsecretPassword!!!", DateTime.Now.AddYears(-7), false);
        userReadonlyRepositoryMock.Setup(x => x.GetByIdAsync(command.Id)).ReturnsAsync(user);

        var userWriteRepositoryMock = new Mock<IUserWriteOnlyRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        var sut = MakeSut(
                    userReadOnlyRepository: userReadonlyRepositoryMock.Object,
                    userWriteOnlyRepository: userWriteRepositoryMock.Object,
                    unitOfWork: unitOfWorkMock.Object
                );

        var result = await sut.HandleAsync(command, CancellationToken.None);

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.Empty(result.Errors);
        Assert.Equal(command.Name, user.Name);
        Assert.Equal(command.Email, user.Email);
        Assert.Equal(command.BirthDate, user.BirthDate);

        userReadonlyRepositoryMock.Verify(x => x.GetByIdAsync(command.Id), Times.Once);
        userWriteRepositoryMock.Verify(x => x.UpdateAsync(user), Times.Once);
        unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }
}
