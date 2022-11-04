using System.Text.RegularExpressions;
using FluentValidation;
using IFCE.AutoGate.Application.Dtos;

namespace IFCE.AutoGate.Application.Validators;

public class VehicleValidator : AbstractValidator<DriverVehicleDto>
{
    public VehicleValidator()
    {
        RuleFor(v => v.Plate)
            .NotNull()
            .WithMessage("A placa do veículo {CollectionIndex} é obrigatório")
            .Must(BeAValidPlate)
            .WithMessage("A placa do veículo {CollectionIndex} deve ser válida");

        RuleFor(v => v.Model)
            .NotNull()
            .WithMessage("O modelo do veículo {CollectionIndex} é obrigatório")
            .MaximumLength(64)
            .WithMessage("O modelo do veículo {CollectionIndex} atingiu o limite máximo de 64 caracteres");

        RuleFor(v => v.CategoryId)
            .NotNull()
            .WithMessage("A categoria do veículo {CollectionIndex} é obrigatório");
    }

    private bool BeAValidPlate(string plate)
    {
        return new Regex("^[a-zA-Z]{3}[0-9][A-Za-z0-9][0-9]{2}$").IsMatch(plate);
    }
}