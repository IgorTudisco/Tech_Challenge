using CloudGame.Domain.Entities;
using CloudGame.Domain.Handlers;
using CloudGame.Domain.Interfaces;

namespace CloudGame.Application.Handlers.UserHandler.Create
{
    public sealed class CreateUserCommandHandler(IUserWriteOnlyRepository userWriteOnlyRepository, IUnitOfWork unitOfWork) : IHandler<CreateUserCommand, CreateUserCommandResponse>
    {
        public async Task<CreateUserCommandResponse> HandleAsync(CreateUserCommand command, CancellationToken cancellationToken)
        {
            User newUser = new(command.Name, command.Email, command.Password, command.BirthDate);

            await userWriteOnlyRepository.AddAsync(newUser);

            await unitOfWork.SaveChangesAsync();

            return new CreateUserCommandResponse(newUser.Id, newUser.Name, newUser.Email);
        }
    }
}
