using System.Threading;

namespace CloudGame.Domain.Handlers;

public interface IHandler<TCommand, TResponse>
    where TCommand : ICommand, new()
    where TResponse : IResponse
{
    Task<TResponse> HandleAsync(TCommand command, CancellationToken cancellationToken);
}
