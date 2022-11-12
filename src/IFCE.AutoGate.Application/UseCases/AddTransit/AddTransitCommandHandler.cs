using FluentValidation;
using IFCE.AutoGate.Core.DomainObjects;
using IFCE.AutoGate.Core.Messages;
using IFCE.AutoGate.Domain.Contracts.Repositories;
using IFCE.AutoGate.Domain.Entities;
using IFCE.AutoGate.Domain.Errors;
using MediatR;
using INotification = IFCE.AutoGate.Core.Contracts.INotification;

namespace IFCE.AutoGate.Application.UseCases.AddTransit;

public class AddTransitCommandHandler : CommandHandler<AddTransitCommand>, IRequestHandler<AddTransitCommand, bool>
{
    private readonly IDriverRepository _driverRepository;

    public AddTransitCommandHandler(IValidator<AddTransitCommand> validator, INotification notification,
        IDriverRepository driverRepository) : base(
        validator, notification)
    {
        _driverRepository = driverRepository;
    }

    public async Task<bool> Handle(AddTransitCommand command, CancellationToken cancellationToken)
    {
        var errors = Validate(command);
        if (errors.Any()) return Failure(new ValidationError(errors));

        var vehicle = await _driverRepository.LoadVehicleByTag(command.CardNumber);

        if (vehicle is null) return Failure(new NotFoundError("Veículo não encontrado"));

        var transit = new Transit(vehicle.DriverId, vehicle.Id,
            Enumeration.FromValue<TransitType>(command.TransitTypeId));

        _driverRepository.AddTransit(transit);
        var isSuccess = await _driverRepository.UnitOfWork.Commit();

        return isSuccess || Failure(new UnexpectedError());
    }
}
