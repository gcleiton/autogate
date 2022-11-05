using IFCE.AutoGate.Core.Messages;
using IFCE.AutoGate.Domain.Contracts.Repositories;
using IFCE.AutoGate.Domain.Errors;
using MediatR;
using INotification = IFCE.AutoGate.Core.Contracts.INotification;

namespace IFCE.AutoGate.Application.UseCases.ReactivateDriver;

public class ReactivateDriverCommandHandler : CommandHandler<ReactivateDriverCommand>,
    IRequestHandler<ReactivateDriverCommand, bool>
{
    private readonly IDriverRepository _driverRepository;

    public ReactivateDriverCommandHandler(INotification notification, IDriverRepository driverRepository) :
        base(notification)
    {
        _driverRepository = driverRepository;
    }

    public async Task<bool> Handle(ReactivateDriverCommand command, CancellationToken cancellationToken)
    {
        var driver = await _driverRepository.LoadBy(d => d.Id == command.Id && d.DisabledAt != null);
        if (driver is null) return Failure(new NotFoundError("Motorista não encontrado"));

        driver.Enable();
        var isSuccess = await _driverRepository.UnitOfWork.Commit();

        return isSuccess || Failure(new UnexpectedError());
    }
}
