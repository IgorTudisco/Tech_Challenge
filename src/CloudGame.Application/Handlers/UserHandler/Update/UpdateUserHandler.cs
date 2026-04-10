using CloudGame.Domain.Commom;
using CloudGame.Domain.Handlers;
using CloudGame.Domain.Interfaces;

namespace CloudGame.Application.Handlers.UserHandler.Update;

public sealed class UpdateUserHandler(
    IUserWriteOnlyRepository userWriteOnlyRepository,
    IUserReadOnlyRepository userReadOnlyRepository,
    IUnitOfWork unitOfWork)
    : IHandler<UpdateUserCommand, UpdateUserResponse>
{
    public async Task<Result<UpdateUserResponse>> HandleAsync(
        UpdateUserCommand command,
        CancellationToken cancellationToken)
    {
        var userToUpdate = await userReadOnlyRepository.GetByIdAsync(command.Id);

        if (userToUpdate is null)
            return Result<UpdateUserResponse>.Failure([new Error("NotFound", "Usuário não encontrado")]);

        userToUpdate.UpdateUser(command.Name, command.Email, command.BirthDate);

        await userWriteOnlyRepository.UpdateAsync(userToUpdate);

        await unitOfWork.SaveChangesAsync();

        return Result<UpdateUserResponse>.Success(new UpdateUserResponse(userToUpdate.Id, userToUpdate.Name, userToUpdate.Email));
    }
}
