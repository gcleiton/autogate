using IFCE.AutoGate.Application.Dtos;
using IFCE.AutoGate.Core.Messages;
using IFCE.AutoGate.Domain.Contracts.Repositories;
using IFCE.AutoGate.Domain.Errors;
using MediatR;
using INotification = IFCE.AutoGate.Core.Contracts.INotification;

namespace IFCE.AutoGate.Application.UseCases.LoadDriverById;

public class LoadDriverByIdQueryHandler : QueryHandler, IRequestHandler<LoadDriverByIdQuery, DriverDto>
{
    private readonly IDriverRepository _driverRepository;

    public LoadDriverByIdQueryHandler(IDriverRepository driverRepository, INotification notification) :
        base(notification)
    {
        _driverRepository = driverRepository;
    }

    public async Task<DriverDto> Handle(LoadDriverByIdQuery query, CancellationToken cancellationToken)
    {
        var driver = await _driverRepository.LoadById(query.Id);

        if (driver is null)
        {
            Failure(new NotFoundError("Motorista não encontrado"));
            return null;
        }

        return DriverDto.FromEntity(driver);
    }
}
