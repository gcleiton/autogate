using FluentValidation;
using IFCE.AutoGate.Application.UseCases.ChangePassword;

namespace IFCE.AutoGate.Application.Validators;

public class ChangePasswordAdministratorValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordAdministratorValidator()
    {
        RuleFor(a => a.Password)
            .NotEmpty().WithMessage("A senha é obrigatória")
            .MinimumLength(6).WithMessage("A senha deve conter no mínimo 6 caracteres")
            .MaximumLength(16).WithMessage("A senha deve conter no máximo 16 caracteres")
            .Matches(@"[A-Z]+").WithMessage("A senha deve conter pelo menos uma letra maiúscula")
            .Matches(@"[a-z]+").WithMessage("A senha deve conter pelo menos uma letra minúscula")
            .Matches(@"[0-9]+").WithMessage("A senha deve conter pelo menos um dígito");

        RuleFor(a => a.PasswordConfirmation)
            .NotEmpty().WithMessage("A confirmação da senha é obrigatória")
            .Equal(a => a.Password).WithMessage("As senhas não coincidem");

        RuleFor(a => a.RecoveryPasswordCode)
            .NotEmpty().WithMessage("O código de recuperação da senha não foi informado");
    }
}
