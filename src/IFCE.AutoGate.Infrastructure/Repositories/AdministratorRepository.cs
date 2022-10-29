using System.Linq.Expressions;
using IFCE.AutoGate.Domain.Contracts.Repositories;
using IFCE.AutoGate.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IFCE.AutoGate.Infrastructure.Repositories;

public class AdministratorRepository : Repository<Administrator>, IAdministratorRepository
{
    public AdministratorRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<bool> Any(Expression<Func<Administrator, bool>> predicate)
    {
        return await _context.Administrators.AnyAsync(predicate);
    }

    public async Task<bool> CheckByEmail(string email)
    {
        return await _context.Administrators.AnyAsync(a => a.Email == email);
    }

    public async Task<Administrator> LoadById(Guid id)
    {
        return await _context.Administrators.FirstOrDefaultAsync(a => a.Id == id);
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
        var test = _context.Entry(administrator).State;
        _context.Administrators.Update(administrator);
    }

    public void Remove(Administrator administrator)
    {
        _context.Administrators.Remove(administrator);
    }

    public async Task<bool> CheckById(Guid id)
    {
        return await _context.Administrators.AnyAsync(a => a.Id == id);
    }
}
