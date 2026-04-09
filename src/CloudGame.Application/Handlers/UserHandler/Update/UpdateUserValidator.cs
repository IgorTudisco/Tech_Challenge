using FluentValidation;

namespace CloudGame.Application.Handlers.UserHandler.Update;

public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("O Id é obrigatório.");
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .MaximumLength(100).WithMessage("O nome deve conter no máximo 100 caracteres.");
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("O email é obrigatório.")
            .EmailAddress().WithMessage("O email deve ser válido.");
        RuleFor(x => x.BirthDate)
            .NotEmpty().WithMessage("A data de nascimento é obrigatória.")
            .LessThan(DateTime.UtcNow).WithMessage("A data de nascimento deve ser menor que a data atual.");
    }
}
