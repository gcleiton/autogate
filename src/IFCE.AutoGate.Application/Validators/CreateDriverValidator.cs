using DocumentValidator;
using FluentValidation;
using IFCE.AutoGate.Application.Dtos;
using IFCE.AutoGate.Application.UseCases.CreateDriver;

namespace IFCE.AutoGate.Application.Validators;

public class CreateDriverValidator : AbstractValidator<CreateDriverCommand>
{
    public CreateDriverValidator()
    {
        RuleFor(c => c.Photo).SetValidator(new PhotoValidator());

        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("O nome é obrigatório")
            .MaximumLength(80)
            .WithMessage("O nome atingiu o tamanho máximo de 80 caracteres");

        RuleFor(c => c.Email)
            .NotEmpty()
            .WithMessage("O e-mail é obrigatório")
            .EmailAddress()
            .WithMessage("O e-mail informado é inválido")
            .MaximumLength(80)
            .WithMessage("O e-mail atingiu o limite máximo de 80 caracteres");

        RuleFor(c => c.License)
            .NotEmpty()
            .WithMessage("O número da licença é obrigatório")
            .Must(BeAValidLicense)
            .WithMessage("O número da licença deve ser válido");

        RuleFor(c => c.Phone)
            .NotEmpty()
            .WithMessage("O telefone é obrigatório")
            .Length(9, 11)
            .WithMessage("O formato de telefone deve ser válido");

        RuleFor(c => c.BirthDate)
            .NotEmpty()
            .WithMessage("A data de nascimento é obrigatório")
            .Must(BeAValidAge)
            .WithMessage("A data de nascimento deve ser válida");

        RuleFor(c => c.Vehicles)
            .NotEmpty()
            .WithMessage("O motorista deve ter pelo menos um veículo associado")
            .Must(BeAValidVehiclesWithUniquePlate)
            .WithMessage("Não é possível cadastrar mais de um veículo com placa igual")
            .Must(BeAValidVehiclesWithUniqueCardNumber)
            .WithMessage("Não é possível cadastrar mais de um veículo com cartão de acesso igual");

        RuleForEach(c => c.Vehicles).SetValidator(new VehicleValidator());
    }

    private bool BeAValidVehiclesWithUniquePlate(IEnumerable<DriverVehicleDto> vehicles)
    {
        return vehicles.Count() == vehicles.Select(vehicle => vehicle.Plate).Distinct().Count();
    }

    private bool BeAValidVehiclesWithUniqueCardNumber(IEnumerable<DriverVehicleDto> vehicles)
    {
        return vehicles.Count() == vehicles.Select(vehicle => vehicle.CardNumber).Distinct().Count();
    }

    private bool BeAValidLicense(string license)
    {
        return CnhValidation.Validate(license);
    }

    private bool BeAValidAge(DateOnly date)
    {
        if (date <= DateOnly.MinValue) return false;

        var currentYear = DateTime.Now.Year;
        var inputYear = date.Year;

        return inputYear <= currentYear && inputYear > currentYear - 125;
    }
}
