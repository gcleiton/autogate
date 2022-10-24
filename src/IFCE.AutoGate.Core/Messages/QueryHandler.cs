using IFCE.AutoGate.Core.Contracts;
using IFCE.AutoGate.Core.DomainObjects;

namespace IFCE.AutoGate.Core.Messages;

public abstract class QueryHandler<TResponse> where TResponse : class
{
    private readonly INotification _notification;

    protected QueryHandler(INotification notification)
    {
        _notification = notification;
    }

    public TResponse Failure(Error error)
    {
        _notification.AddError(error);
        return null;
    }
}
