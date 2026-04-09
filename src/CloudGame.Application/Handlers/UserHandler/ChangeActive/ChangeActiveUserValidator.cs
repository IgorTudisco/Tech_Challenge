using FluentValidation;

namespace CloudGame.Application.Handlers.UserHandler.ChangeActive;

public class ChangeActiveUserValidator : AbstractValidator<ChangeActiveUserCommand>
{
    public ChangeActiveUserValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("O Id é obrigatório.");
    }
}
