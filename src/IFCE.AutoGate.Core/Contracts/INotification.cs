using IFCE.AutoGate.Core.DomainObjects;

namespace IFCE.AutoGate.Core.Contracts;

public interface INotification
{
    Error Error { get; }
    void AddError(Error error);
    public bool HasError();
}
