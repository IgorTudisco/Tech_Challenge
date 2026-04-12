namespace CloudGame.Application.Handlers.UserHandler.Find;

public record FindUsersQueryResponse(int Id, string UserName, string UserEmail, bool IsAdmin, bool Active);
