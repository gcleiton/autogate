using IFCE.AutoGate.Core.Messages;

namespace IFCE.AutoGate.Application.Events.TransitAuthorized;

public class TransitAuthorizedEvent : Event
{
    public TransitAuthorizedEvent(Guid transitId)
    {
        TransitId = transitId;
    }

    public Guid TransitId { get; }
}
