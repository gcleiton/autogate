using IFCE.AutoGate.Core.Contracts;
using IFCE.AutoGate.Core.DomainObjects;

namespace IFCE.AutoGate.Domain.Contracts;

public interface IRepository<T> : IDisposable where T : IAggregateRoot
{
    IUnitOfWork UnitOfWork { get; }
}
