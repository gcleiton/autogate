using IFCE.AutoGate.Application.Dtos;
using IFCE.AutoGate.Core.Communication;
using IFCE.AutoGate.Core.Contracts;
using IFCE.AutoGate.Core.Messages;
using IFCE.AutoGate.Infrastructure;
using MediatR;

namespace IFCE.AutoGate.Application.UseCases.LoadDrivers;

public class LoadDriversQueryHandler : QueryHandler, IRequestHandler<LoadDriversQuery, IPaginatedResult<DriverDto>>
{
    private readonly ApplicationDbContext _dbContext;


    public LoadDriversQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IPaginatedResult<DriverDto>> Handle(LoadDriversQuery query, CancellationToken cancellationToken)
    {
        var queryable = _dbContext.Drivers.AsQueryable();

        query.ApplyFilter(ref queryable);
        query.ApplyOrder(ref queryable);

        var paginatedList = query.ToPaginatedList(ref queryable);

        return new PaginatedResult<DriverDto>(paginatedList);
    }
}
