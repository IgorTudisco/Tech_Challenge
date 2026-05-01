using CloudGame.Domain.Commom;
using CloudGame.Domain.Handlers;
using CloudGame.Domain.Interfaces;

namespace CloudGame.Application.Handlers.UserHandler.Delete;

public class DeleteUserHandler(
        IUserWriteOnlyRepository userWriteOnlyRepository,
        IUserReadOnlyRepository userReadOnlyRepository,
        IUnitOfWork unitOfWork) : IHandler<DeleteUserCommand>
{
    public async Task<Result> HandleAsync(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        if (command.Id <= 0)
            return Result.Failure([new("NotFound", "Usuário não encontrado")]);

        var userToBeDeleted = await userReadOnlyRepository.GetByIdAsync(command.Id);

        if (userToBeDeleted is null)
            return Result.Failure([new("NotFound", "Usuário não encontrado")]);

        userToBeDeleted.DeleteData();

        await userWriteOnlyRepository.UpdateAsync(userToBeDeleted);

        await unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}