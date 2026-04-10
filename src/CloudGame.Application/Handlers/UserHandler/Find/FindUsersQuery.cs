using CloudGame.Domain.Handlers;
using CloudGame.Domain.Parameters;

namespace CloudGame.Application.Handlers.UserHandler.Find;

public sealed class FindUsersQuery: ICommand
{
    public FindUsersQuery()
    {
        Parameters = new FindUsersParameter();
    }

    public FindUsersQuery(FindUsersParameter parameters)
    {
        Parameters = parameters;
    }
    public FindUsersParameter Parameters { get; set; }
}
