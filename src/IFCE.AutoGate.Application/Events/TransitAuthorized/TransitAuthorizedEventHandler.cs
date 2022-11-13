using IFCE.AutoGate.Application.Dtos;
using IFCE.AutoGate.Application.Hubs;
using IFCE.AutoGate.Domain.Contracts.Repositories;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace IFCE.AutoGate.Application.Events.TransitAuthorized;

public class TransitAuthorizedEventHandler : INotificationHandler<TransitAuthorizedEvent>
{
    private readonly IDriverRepository _driverRepository;
    private readonly IHubContext<TransitHub> _hubContext;

    public TransitAuthorizedEventHandler(IHubContext<TransitHub> hubContext, IDriverRepository driverRepository)
    {
        _hubContext = hubContext;
        _driverRepository = driverRepository;
    }

    public async Task Handle(TransitAuthorizedEvent message, CancellationToken cancellationToken)
    {
        var transit = await _driverRepository.LoadTransitById(message.TransitId);
        var transitTotal = await _driverRepository.CountTransitQuantityByDriverId(transit.DriverId);

        var result = new AuthorizedTransitDto
        {
            Name = transit.Driver.Name,
            Email = transit.Driver.Email,
            Phone = transit.Driver.Phone,
            License = transit.Driver.License,
            VehiclePlate = transit.Vehicle.Plate,
            VehicleModel = transit.Vehicle.Model,
            VehicleCategory = transit.Vehicle.Category.Name,
            TransitType = transit.TransitTypeId,
            TransitTotal = transitTotal
        };

        await _hubContext.Clients.All.SendAsync("AuthorizedTransit", result, cancellationToken);
    }
}
