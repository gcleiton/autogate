using IFCE.AutoGate.Application.Results;
using IFCE.AutoGate.Core.Communication;
using IFCE.AutoGate.Core.Contracts;
using IFCE.AutoGate.Core.Messages;
using IFCE.AutoGate.Infrastructure;
using MediatR;
using INotification = IFCE.AutoGate.Core.Contracts.INotification;

namespace IFCE.AutoGate.Application.UseCases.LoadAdministrators;

public class LoadAdministratorsQueryHandler : QueryHandler<LoadAdministratorsQuery>,
    IRequestHandler<LoadAdministratorsQuery, IPaginatedResult<AdministratorResult>>
{
    private readonly ApplicationDbContext _dbContext;

    public LoadAdministratorsQueryHandler(INotification notification, ApplicationDbContext dbContext)
        : base(notification)
    {
        _dbContext = dbContext;
    }

    public async Task<IPaginatedResult<AdministratorResult>> Handle(LoadAdministratorsQuery query,
        CancellationToken cancellationToken)
    {
        var queryable = _dbContext.Administrators.AsQueryable();

        query.ApplyFilter(ref queryable);
        query.ApplyOrder(ref queryable);

        var paginatedList = query.ToPaginatedList(ref queryable);

        return new PaginatedResult<AdministratorResult>(paginatedList);
    }
}
