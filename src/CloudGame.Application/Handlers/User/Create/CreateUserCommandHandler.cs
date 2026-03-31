using CloudGame.Domain.Handlers;

namespace CloudGame.Application.Handlers.User.Create
{
    public sealed class CreateUserCommandHandler : IHandler<CreateUserCommand, CreateUserCommandResponse>
    {
        public Task<CreateUserCommandResponse> HandleAsync(CreateUserCommand command, CancellationToken cancellationToken)
        {
            return new CreateUserCommandResponse();
        }        
    }
}
