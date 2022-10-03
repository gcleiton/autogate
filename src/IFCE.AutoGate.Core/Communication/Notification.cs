using IFCE.AutoGate.Core.Contracts;
using IFCE.AutoGate.Core.DomainObjects;

namespace IFCE.AutoGate.Core.Communication;

public class Notification : INotification
{
    public Error Error { get; private set; }

    public void AddError(Error error)
    {
        Error = error;
    }

    public bool HasError()
    {
        return Error is not null;
    }
}
