using FluentValidation;
using IFCE.AutoGate.Application.Requests;

namespace IFCE.AutoGate.Application.Validators;

public class LoginValidator : AbstractValidator<LoginRequest>
{
    public LoginValidator()
    {
        RuleFor(l => l.Email)
            .NotEmpty().WithMessage("O e-mail é obrigatório")
            .EmailAddress().WithMessage("O e-mail informado é inválido");

        RuleFor(l => l.Password)
            .NotEmpty().WithMessage("A senha é obrigatório");
    }
}
