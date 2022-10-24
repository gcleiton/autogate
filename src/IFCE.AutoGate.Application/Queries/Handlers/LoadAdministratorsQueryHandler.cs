using IFCE.AutoGate.Core.Communication;
using IFCE.AutoGate.Core.Contracts;
using IFCE.AutoGate.Core.Messages;
using IFCE.AutoGate.Domain.Contracts.Repositories;
using IFCE.AutoGate.Domain.Entities;
using MediatR;
using INotification = IFCE.AutoGate.Core.Contracts.INotification;

namespace IFCE.AutoGate.Application.Queries.Handlers;

public class LoadAdministratorsQueryHandler : QueryHandler<LoadAdministratorsQuery>,
    IRequestHandler<LoadAdministratorsQuery, IPaginationResult<Administrator>>
{
    private readonly IAdministratorRepository _administratorRepository;

    public LoadAdministratorsQueryHandler(INotification notification, IAdministratorRepository administratorRepository)
        : base(notification)
    {
        _administratorRepository = administratorRepository;
    }

    public async Task<IPaginationResult<Administrator>> Handle(LoadAdministratorsQuery query,
        CancellationToken cancellationToken)
    {
        var administrators = await _administratorRepository.LoadAll(query);

        return new PaginationResult<Administrator>
        {
            Items = administrators,
            Pagination = administrators.ToPagination()
        };
    }
}
