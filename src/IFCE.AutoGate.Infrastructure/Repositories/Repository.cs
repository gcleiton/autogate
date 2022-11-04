using System.Linq.Expressions;
using IFCE.AutoGate.Core.Contracts;
using IFCE.AutoGate.Core.DomainObjects;
using IFCE.AutoGate.Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace IFCE.AutoGate.Infrastructure.Repositories;

public abstract class Repository<T> : IRepository<T> where T : EntityBase, IAggregateRoot
{
    protected readonly ApplicationDbContext _context;
    private readonly DbSet<T> _dbSet;

    protected Repository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public IUnitOfWork UnitOfWork => _context;

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task<bool> CheckBy(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.AnyAsync(predicate);
    }
}
