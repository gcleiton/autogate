using IFCE.AutoGate.Domain.Contracts.Repositories;
using IFCE.AutoGate.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IFCE.AutoGate.Infrastructure.Repositories;

public class AdministratorRepository : Repository<Administrator>, IAdministratorRepository
{
    public AdministratorRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<bool> CheckByEmail(string email)
    {
        return await _context.Administrators.AnyAsync(a => a.Email == email);
    }

    public void Add(Administrator administrator)
    {
        _context.Administrators.Add(administrator);
    }
}
