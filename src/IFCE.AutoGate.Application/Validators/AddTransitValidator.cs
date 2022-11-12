using FluentValidation;
using IFCE.AutoGate.Application.UseCases.AddTransit;
using IFCE.AutoGate.Core.DomainObjects;
using IFCE.AutoGate.Domain.Entities;

namespace IFCE.AutoGate.Application.Validators;

public class AddTransitValidator : AbstractValidator<AddTransitCommand>
{
    public AddTransitValidator()
    {
        RuleFor(c => c.CardNumber).NotEmpty().WithMessage("O número do cartão é obrigatório");
        RuleFor(c => c.TransitTypeId).NotEmpty().WithMessage("O tipo de movimentação é obrigatório")
            .Must(BeAValidTransitType).WithMessage("O tipo de trânsito deve ser válido");
    }

    private bool BeAValidTransitType(int id)
    {
        try
        {
            Enumeration.FromValue<TransitType>(id);
            return true;
        }
        catch (Exception _)
        {
            return false;
        }
    }
}
