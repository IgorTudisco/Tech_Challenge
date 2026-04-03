using CloudGame.Domain.Entities;
using CloudGame.Domain.Handlers;
using CloudGame.Domain.Interfaces;
using CloudGame.Domain.Interfaces.Security;

namespace CloudGame.Application.Handlers.UserHandler.Create;

public sealed class CreateUserCommandHandler(IUserWriteOnlyRepository userWriteOnlyRepository,
    IPasswordHasher passwordHasher, IUnitOfWork unitOfWork) : IHandler<CreateUserCommand, CreateUserCommandResponse>
{
    public async Task<CreateUserCommandResponse> HandleAsync(CreateUserCommand command, CancellationToken cancellationToken)
    {
        string passwordHash = passwordHasher.CreateHash(command.Password);

        User newUser = new(command.Name, command.Email, passwordHash, command.BirthDate, false);

        await userWriteOnlyRepository.AddAsync(newUser);

        await unitOfWork.SaveChangesAsync();

        return new CreateUserCommandResponse(newUser.Id, newUser.Name, newUser.Email);
    }
}
