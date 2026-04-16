using CloudGame.Domain.Handlers;

namespace CloudGame.Application.Handlers.UserHandler.Update;

public class UpdateUserResponse(int Id, string Nome, string Email) : IResponse;
