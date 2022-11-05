using IFCE.AutoGate.Core.Messages;
using IFCE.AutoGate.Domain.Contracts.Repositories;
using IFCE.AutoGate.Domain.Errors;
using MediatR;
using INotification = IFCE.AutoGate.Core.Contracts.INotification;

namespace IFCE.AutoGate.Application.UseCases.DeleteDriver;

public class DeleteDriverCommandHandler : CommandHandler<DeleteDriverCommand>,
    IRequestHandler<DeleteDriverCommand, bool>
{
    private readonly IDriverRepository _driverRepository;

    public DeleteDriverCommandHandler(INotification notification, IDriverRepository driverRepository) :
        base(notification)
    {
        _driverRepository = driverRepository;
    }

    public async Task<bool> Handle(DeleteDriverCommand command, CancellationToken cancellationToken)
    {
        var driver = await _driverRepository.LoadBy(d => d.Id == command.Id);
        if (driver is null) return Failure(new NotFoundError("Motorista não encontrado"));

        driver.Disable();
        var isSuccess = await _driverRepository.UnitOfWork.Commit();

        return isSuccess || Failure(new UnexpectedError());
    }
}
