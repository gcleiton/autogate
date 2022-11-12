using FluentValidation;
using IFCE.AutoGate.Core.DomainObjects;
using IFCE.AutoGate.Core.Messages;
using IFCE.AutoGate.Domain.Contracts.Gateways;
using IFCE.AutoGate.Domain.Contracts.Repositories;
using IFCE.AutoGate.Domain.Entities;
using IFCE.AutoGate.Domain.Errors;
using MediatR;
using INotification = IFCE.AutoGate.Core.Contracts.INotification;

namespace IFCE.AutoGate.Application.UseCases.CreateDriver;

public class CreateDriverCommandHandler : CommandHandler<CreateDriverCommand>,
    IRequestHandler<CreateDriverCommand, bool>
{
    private readonly IDriverRepository _driverRepository;
    private readonly IFileStorage _fileStorage;

    public CreateDriverCommandHandler(IValidator<CreateDriverCommand> validator, INotification notification,
        IDriverRepository driverRepository, IFileStorage fileStorage) : base(
        validator, notification)
    {
        _driverRepository = driverRepository;
        _fileStorage = fileStorage;
    }

    public async Task<bool> Handle(CreateDriverCommand command, CancellationToken cancellationToken)
    {
        var errors = Validate(command);
        if (errors.Any()) return Failure(new ValidationError(errors));

        var error = await CanHandle(command);
        if (error is not null) return Failure(error);

        var driver = MapDriver(command);

        if (command.Photo is not null && command.Photo.Length > 0)
        {
            var fileKey = await _fileStorage.Upload(command.Photo);
            driver.ChangePhoto(fileKey);
        }

        _driverRepository.Add(driver);
        var isSuccess = await _driverRepository.UnitOfWork.Commit();

        return isSuccess || Failure(new UnexpectedError());
    }

    private async Task<Error> CanHandle(CreateDriverCommand command)
    {
        var emailAlreadyExists = await _driverRepository.CheckBy(d => d.Email == command.Email);
        if (emailAlreadyExists)
            return new AlreadyExistsError("O e-mail informado já está em uso por outro motorista.");

        var tagAlreadyExists = await _driverRepository.CheckBy(d => d.Tag == command.CardNumber);
        if (tagAlreadyExists)
            return new AlreadyExistsError("O número do cartão já está em uso por outro motorista.");

        var licenseAlreadyExists = await _driverRepository.CheckBy(d => d.License == command.License);
        if (licenseAlreadyExists)
            return new AlreadyExistsError("A licença já está em uso por outro motorista.");

        var plates = command.Vehicles.Select(v => v.Plate);
        var anyPlateAlreadyExists = await _driverRepository.CheckByVehiclePlates(plates);
        if (anyPlateAlreadyExists)
            return new AlreadyExistsError("Um ou mais veículos já está em uso por outro motorista.");

        return null;
    }

    private Driver MapDriver(CreateDriverCommand command)
    {
        var vehicles = command.Vehicles.Select(dto => new Vehicle(dto.Plate, dto.Model, dto.CategoryId));
        var driver = new Driver(command.Name, command.Email, command.BirthDate, command.Phone, command.License,
            command.CardNumber, vehicles);

        return driver;
    }
}
