using FluentValidation;
using IFCE.AutoGate.Application.UseCases.CreateAdministrator;

namespace IFCE.AutoGate.Application.Validators;

public class CreateAdministratorValidator : AbstractValidator<CreateAdministratorCommand>
{
    public CreateAdministratorValidator()
    {
        RuleFor(a => a.Name)
            .NotEmpty()
            .WithMessage("O nome do administrador é obrigatório")
            .MaximumLength(150)
            .WithMessage("O nome do administrador atingiu o tamanho máximo de 128 caracteres");

        RuleFor(a => a.Email)
            .NotEmpty()
            .WithMessage("O e-mail do administrador é obrigatório")
            .MaximumLength(80)
            .WithMessage("O e-mail do administrador atingiu o tamanho máximo de 80 caracteres")
            .EmailAddress()
            .WithMessage("O e-mail do administrador informado é inválido");
    }
}
