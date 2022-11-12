using IFCE.AutoGate.Core.Contracts;
using IFCE.AutoGate.Core.DomainObjects;

namespace IFCE.AutoGate.Core.Messages;

public abstract class QueryHandler
{
    private readonly INotification _notification;

    protected QueryHandler()
    {
    }

    protected QueryHandler(INotification notification)
    {
        _notification = notification;
    }

    public void Failure(Error error)
    {
        _notification.AddError(error);
    }
}
