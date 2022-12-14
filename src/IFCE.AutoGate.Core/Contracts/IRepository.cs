using System.Linq.Expressions;
using IFCE.AutoGate.Core.Contracts;
using IFCE.AutoGate.Core.DomainObjects;

namespace IFCE.AutoGate.Domain.Contracts;

public interface IRepository<T> : IDisposable where T : IAggregateRoot
{
    IUnitOfWork UnitOfWork { get; }
    Task<bool> CheckBy(Expression<Func<T, bool>> predicate);
    Task<T?> LoadBy(Expression<Func<T, bool>> predicate);
}
