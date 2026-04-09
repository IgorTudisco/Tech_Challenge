using CloudGame.Domain.Handlers;
using CloudGame.Domain.Interfaces;

namespace CloudGame.Application.Handlers.UserHandler.Update;

public sealed class UpdateUserHandler(
    IUserWriteOnlyRepository userWriteOnlyRepository,
    IUserReadOnlyRepository userReadOnlyRepository,
    IUnitOfWork unitOfWork)
    : IHandler<UpdateUserCommand, UpdateUserResponse>
{
    public async Task<UpdateUserResponse> HandleAsync(
        UpdateUserCommand command,
        CancellationToken cancellationToken)
    {
        var userToUpdate = await userReadOnlyRepository.GetByIdAsync(command.Id);

        userToUpdate.UpdateUser(command.Name, command.Email, command.BirthDate);

        await userWriteOnlyRepository.UpdateAsync(userToUpdate);

        await unitOfWork.SaveChangesAsync();

        return new UpdateUserResponse(userToUpdate.Id, userToUpdate.Name, userToUpdate.Email);
    }
}
