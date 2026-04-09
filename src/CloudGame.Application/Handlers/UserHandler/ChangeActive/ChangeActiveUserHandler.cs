using CloudGame.Domain.Handlers;
using CloudGame.Domain.Interfaces;

namespace CloudGame.Application.Handlers.UserHandler.ChangeActive;

public sealed class ChangeActiveUserHandler(
    IUserWriteOnlyRepository userWriteOnlyRepository,
    IUserReadOnlyRepository userReadOnlyRepository,
    IUnitOfWork unitOfWork) : IHandler<ChangeActiveUserCommand, ChangeActiveUserResponse>
{
    public async Task<ChangeActiveUserResponse> HandleAsync(
        ChangeActiveUserCommand command,
        CancellationToken cancellationToken)
    {
        var userToUpdate = await userReadOnlyRepository.GetByIdAsync(command.Id);

        userToUpdate.SetActive(!userToUpdate.Active);

        await userWriteOnlyRepository.UpdateAsync(userToUpdate);

        await unitOfWork.SaveChangesAsync();

        return new ChangeActiveUserResponse();
    }
}
