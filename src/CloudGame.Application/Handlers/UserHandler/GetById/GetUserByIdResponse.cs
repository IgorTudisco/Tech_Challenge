using CloudGame.Domain.Handlers;

namespace CloudGame.Application.Handlers.UserHandler.GetById;

public readonly record struct GetUserByIdResponse(int Id, string Name, string Email, DateTime BirthDate, DateTime CreatedAt, DateTime? UpdateAt) : IResponse;


