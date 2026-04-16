using CloudGame.Domain.Commom;
using CloudGame.Domain.Handlers;
using CloudGame.Domain.Interfaces;

namespace CloudGame.Application.Handlers.UserHandler.Find;

public sealed class FindUsersQueryHandler(IUserReadOnlyRepository userReadOnlyRepository) : IHandler<FindUsersQuery, Pagination<FindUsersQueryResponse>>
{
    public async Task<Result<Pagination<FindUsersQueryResponse>>> HandleAsync(FindUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await userReadOnlyRepository.FindAsync(request.Parameters);

        var usersResponse = users.Items.Select(s => new FindUsersQueryResponse(s.Id, s.Name, s.Email, s.IsAdmin, s.Active)).ToList();

        return Result<Pagination<FindUsersQueryResponse>>.Success(new Pagination<FindUsersQueryResponse>(usersResponse, users.Count));
    }
}
