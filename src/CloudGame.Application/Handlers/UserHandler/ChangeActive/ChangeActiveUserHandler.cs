using CloudGame.Application.Handlers.UserHandler.Update;
using CloudGame.Domain.Commom;
using CloudGame.Domain.Handlers;
using CloudGame.Domain.Interfaces;

namespace CloudGame.Application.Handlers.UserHandler.ChangeActive;

public sealed class ChangeActiveUserHandler(
    IUserWriteOnlyRepository userWriteOnlyRepository,
    IUserReadOnlyRepository userReadOnlyRepository,
    IUnitOfWork unitOfWork) : IHandler<ChangeActiveUserCommand>
{
    public async Task<Result> HandleAsync(
        ChangeActiveUserCommand command,
        CancellationToken cancellationToken)
    {
        var userToUpdate = await userReadOnlyRepository.GetByIdAsync(command.Id);

        if (userToUpdate is null)
            return Result.Failure([new Error("NotFound", "Usuário não encontrado")]);

        userToUpdate.SetActive(!userToUpdate.Active);

        await userWriteOnlyRepository.UpdateAsync(userToUpdate);

        await unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
