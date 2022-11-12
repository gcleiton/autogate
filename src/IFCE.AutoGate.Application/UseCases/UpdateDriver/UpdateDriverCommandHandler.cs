using FluentValidation;
using IFCE.AutoGate.Core.DomainObjects;
using IFCE.AutoGate.Core.Messages;
using IFCE.AutoGate.Domain.Contracts.Repositories;
using IFCE.AutoGate.Domain.Entities;
using IFCE.AutoGate.Domain.Errors;
using MediatR;
using INotification = IFCE.AutoGate.Core.Contracts.INotification;

namespace IFCE.AutoGate.Application.UseCases.UpdateDriver;

public class UpdateDriverCommandHandler : CommandHandler<UpdateDriverCommand>,
    IRequestHandler<UpdateDriverCommand, bool>
{
    private readonly IDriverRepository _driverRepository;

    public UpdateDriverCommandHandler(IValidator<UpdateDriverCommand> validator, INotification notification,
        IDriverRepository driverRepository) : base(validator, notification)
    {
        _driverRepository = driverRepository;
    }

    public async Task<bool> Handle(UpdateDriverCommand command, CancellationToken cancellationToken)
    {
        var errors = Validate(command);
        if (errors.Any()) return Failure(new ValidationError(errors));

        var error = await CanHandle(command);
        if (error is not null) return Failure(error);

        var driver = MapDriver(command);
        _driverRepository.Update(driver);
        var isSuccess = await _driverRepository.UnitOfWork.Commit();

        return isSuccess || Failure(new UnexpectedError());
    }

    private async Task<Error> CanHandle(UpdateDriverCommand command)
    {
        var driverExists = await _driverRepository.CheckBy(d => d.Id == command.Id);
        if (!driverExists) return new NotFoundError("Motorista não foi encontrado");

        var emailAlreadyExists = await _driverRepository.CheckBy(d => d.Email == command.Email && d.Id != command.Id);
        if (emailAlreadyExists)
            return new AlreadyExistsError("O e-mail informado já está em uso por outro motorista.");

        var tagAlreadyExists = await _driverRepository.CheckBy(d => d.Tag == command.CardNumber && d.Id != command.Id);
        if (tagAlreadyExists)
            return new AlreadyExistsError("O número do cartão já está em uso por outro motorista.");

        var licenseAlreadyExists =
            await _driverRepository.CheckBy(d => d.License == command.License && d.Id != command.Id);
        if (licenseAlreadyExists)
            return new AlreadyExistsError("A licença já está em uso por outro motorista.");

        var plates = command.Vehicles.Select(v => v.Plate);
        var anyPlateAlreadyExists = await _driverRepository.CheckByVehiclePlates(plates, command.Id);
        if (anyPlateAlreadyExists)
            return new AlreadyExistsError("Um ou mais veículos já está em uso por outro motorista.");

        return null;
    }

    private Driver MapDriver(UpdateDriverCommand command)
    {
        var vehicles = command.Vehicles.Select(dto => new Vehicle(dto.Plate, dto.Model, dto.CategoryId, dto.Id));
        var driver = new Driver(command.Name, command.Email, command.BirthDate, command.Phone, command.License,
            command.CardNumber, vehicles, command.Id);

        return driver;
    }
}
