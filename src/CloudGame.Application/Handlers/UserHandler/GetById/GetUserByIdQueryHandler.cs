using CloudGame.Domain.Commom;
using CloudGame.Domain.Handlers;
using CloudGame.Domain.Interfaces;

namespace CloudGame.Application.Handlers.UserHandler.GetById;

public sealed class GetUserByIdQueryHandler(IUserReadOnlyRepository userReadOnlyRepository) : IHandler<GetUserByIdQuery, GetUserByIdResponse>
{
    public async Task<Result<GetUserByIdResponse>> HandleAsync(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userReadOnlyRepository.GetByIdAsync(request.Id);

        if (user is null)
            return Result<GetUserByIdResponse>.Failure([new("NotFound", "Não foi encontrado usuário com id passado.")]);

        return Result<GetUserByIdResponse>.Success(new GetUserByIdResponse(user.Id, user.Name, user.Email, user.BirthDate, user.CreatedAt, user.UpdateAt));
    }
}
