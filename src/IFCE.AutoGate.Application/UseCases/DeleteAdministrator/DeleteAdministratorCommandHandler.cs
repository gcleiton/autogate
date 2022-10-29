using IFCE.AutoGate.Core.Messages;
using IFCE.AutoGate.Domain.Contracts.Repositories;
using IFCE.AutoGate.Domain.Errors;
using MediatR;
using INotification = IFCE.AutoGate.Core.Contracts.INotification;

namespace IFCE.AutoGate.Application.UseCases.DeleteAdministrator;

public class DeleteAdministratorCommandHandler : CommandHandler<DeleteAdministratorCommand>,
    IRequestHandler<DeleteAdministratorCommand, bool>
{
    private readonly IAdministratorRepository _administratorRepository;

    public DeleteAdministratorCommandHandler(INotification notification,
        IAdministratorRepository administratorRepository) : base(notification)
    {
        _administratorRepository = administratorRepository;
    }

    public async Task<bool> Handle(DeleteAdministratorCommand request, CancellationToken cancellationToken)
    {
        var administrator = await _administratorRepository.LoadById(request.Id);

        if (administrator == null) return Failure(new NotFoundError("Administrador não encontrado"));

        _administratorRepository.Remove(administrator);
        var isSuccess = await _administratorRepository.UnitOfWork.Commit();

        return isSuccess || Failure(new UnexpectedError());
    }
}
