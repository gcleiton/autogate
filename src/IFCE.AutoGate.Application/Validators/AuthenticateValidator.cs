using FluentValidation;
using IFCE.AutoGate.Application.UseCases.Authenticate;

namespace IFCE.AutoGate.Application.Validators;

public class AuthenticateValidator : AbstractValidator<AuthenticateCommand>
{
    public AuthenticateValidator()
    {
        RuleFor(l => l.Email)
            .NotEmpty().WithMessage("O e-mail é obrigatório")
            .EmailAddress().WithMessage("O e-mail informado é inválido");

        RuleFor(l => l.Password)
            .NotEmpty().WithMessage("A senha é obrigatório");
    }
}
