using CloudGame.Domain.Handlers;

namespace CloudGame.Application.Handlers.UserHandler.GetById;

public sealed class GetUserByIdQuery : ICommand
{
    public int Id { get; set; }
}
