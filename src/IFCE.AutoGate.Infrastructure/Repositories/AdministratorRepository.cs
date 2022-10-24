using IFCE.AutoGate.Core.Contracts;
using IFCE.AutoGate.Core.DomainObjects;
using IFCE.AutoGate.Core.Messages;
using IFCE.AutoGate.Domain.Contracts.Repositories;
using IFCE.AutoGate.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IFCE.AutoGate.Infrastructure.Repositories;

public class AdministratorRepository : Repository<Administrator>, IAdministratorRepository
{
    public AdministratorRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<PaginatedList<Administrator>> LoadAll(
        PaginatedQuery<Administrator, IPaginationResult<Administrator>> query)
    {
        var queryable = _context.Administrators.AsQueryable();

        query.ApplyFilter(ref queryable);
        query.ApplyOrder(ref queryable);

        return await Task.FromResult(new PaginatedList<Administrator>(queryable, query.Page, query.PageSize));
    }

    public async Task<bool> CheckByEmail(string email)
    {
        return await _context.Administrators.AnyAsync(a => a.Email == email);
    }

    public async Task<Administrator> LoadByEmail(string email)
    {
        return await _context.Administrators.FirstOrDefaultAsync(a => a.Email == email);
    }

    public async Task<Administrator> LoadByRecoveryPasswordCode(Guid code)
    {
        return await _context.Administrators.FirstOrDefaultAsync(a => a.RecoveryPasswordCode == code);
    }

    public void Add(Administrator administrator)
    {
        _context.Administrators.Add(administrator);
    }

    public void Update(Administrator administrator)
    {
        _context.Administrators.Update(administrator);
    }
}
