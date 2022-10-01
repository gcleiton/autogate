using IFCE.AutoGate.Core.Contracts;
using IFCE.AutoGate.Core.DomainObjects;
using IFCE.AutoGate.Domain.Contracts;

namespace IFCE.AutoGate.Infrastructure.Repositories;

public abstract class Repository<T> : IRepository<T> where T : IEntity, IAggregateRoot
{
    protected readonly ApplicationDbContext _context;

    protected Repository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public void Dispose()
    {
        _context.Dispose();
    }
}
