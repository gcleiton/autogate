using FluentValidation;
using IFCE.AutoGate.Application.UseCases.RecoverPassword;

namespace IFCE.AutoGate.Application.Validators;

public class RecoverPasswordValidator : AbstractValidator<RecoverPasswordCommand>
{
    public RecoverPasswordValidator()
    {
        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("O e-mail é obrigatório")
            .EmailAddress().WithMessage("O e-mail informado é inválido");
    }
}
