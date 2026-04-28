using CloudGame.Domain.Commom;
using CloudGame.Domain.Entities;
using CloudGame.Domain.Handlers;
using CloudGame.Domain.Interfaces;
using CloudGame.Domain.Interfaces.Security;

namespace CloudGame.Application.Handlers.UserHandler.Create;

public sealed class CreateUserCommandHandler(
    IUserWriteOnlyRepository userWriteOnlyRepository,
    IPasswordHasher passwordHasher,
    IUserReadOnlyRepository userReadOnlyRepository,
    IUnitOfWork unitOfWork) : IHandler<CreateUserCommand, CreateUserCommandResponse>
{
    public async Task<Result<CreateUserCommandResponse>> HandleAsync(
        CreateUserCommand command,
        CancellationToken cancellationToken)
    {
        string passwordHash = passwordHasher.CreateHash(command.Password);

        var isEmailInUse = await userReadOnlyRepository.CheckIfIsEmailBeingUsedAsync(command.Email);

        if (isEmailInUse)
        {
            return Result<CreateUserCommandResponse>.Failure([new Error("Email", "Email is already in use")]);
        }

        User newUser = new(command.Name, command.Email, passwordHash, command.BirthDate, false);

        await userWriteOnlyRepository.AddAsync(newUser);

        await unitOfWork.SaveChangesAsync();

        return Result<CreateUserCommandResponse>.Success(new CreateUserCommandResponse(newUser.Id, newUser.Name, newUser.Email));
    }
}
