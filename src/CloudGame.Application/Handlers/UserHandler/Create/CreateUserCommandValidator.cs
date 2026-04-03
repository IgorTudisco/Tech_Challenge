using FluentValidation;
using System.Text.RegularExpressions;

namespace CloudGame.Application.Handlers.UserHandler.Create;

public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{  
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .MaximumLength(100).WithMessage("O nome deve conter no máximo 100 caracteres.");
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("O email é obrigatório.")
            .EmailAddress().WithMessage("O email deve ser válido.");
        RuleFor(x => x.Password)
            .NotEmpty()
                .WithMessage("Senha é obrigatória.")
            .MinimumLength(8)
                .WithMessage("A senha deve ter no mínimo 8 caracteres.")
            .Matches(@"[a-z]")
                .WithMessage("A senha deve conter ao menos uma letra minúscula.")
            .Matches(@"[A-Z]")
                .WithMessage("A senha deve conter ao menos uma letra maiúscula.")
            .Matches(@"\d")
                .WithMessage("A senha deve conter ao menos um número.")
            .Matches(@"[@$!%*?&]")
                .WithMessage("A senha deve conter ao menos um caractere especial (@$!%*?&).");
        RuleFor(x => x.BirthDate)
            .NotEmpty().WithMessage("A data de nascimento é obrigatória.")
            .LessThan(DateTime.UtcNow).WithMessage("A data de nascimento deve ser menor que a data atual.");
    }
}
