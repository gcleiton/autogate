using IFCE.AutoGate.Application.Results;
using IFCE.AutoGate.Core.Messages;
using IFCE.AutoGate.Domain.Contracts.Repositories;
using IFCE.AutoGate.Domain.Errors;
using MediatR;
using INotification = IFCE.AutoGate.Core.Contracts.INotification;

namespace IFCE.AutoGate.Application.UseCases.LoadAdministratorById;

public class LoadAdministratorByIdQueryHandler : QueryHandler,
    IRequestHandler<LoadAdministratorByIdQuery, AdministratorResult>
{
    private readonly IAdministratorRepository _administratorRepository;

    public LoadAdministratorByIdQueryHandler(INotification notification,
        IAdministratorRepository administratorRepository) : base(notification)
    {
        _administratorRepository = administratorRepository;
    }

    public async Task<AdministratorResult> Handle(LoadAdministratorByIdQuery query, CancellationToken cancellationToken)
    {
        var administrator = await _administratorRepository.LoadById(query.Id);

        if (administrator == null)
        {
            AddError(new NotFoundError("Administrador não encontrado"));
            return null;
        }

        return new AdministratorResult
        {
            Id = administrator.Id,
            Name = administrator.Name,
            Email = administrator.Email
        };
    }
}
