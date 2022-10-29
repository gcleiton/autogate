namespace IFCE.AutoGate.Core.Contracts;

public interface IUnitOfWork
{
    public Task<bool> Commit();
}
