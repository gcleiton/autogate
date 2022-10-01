using MediatR;

namespace IFCE.AutoGate.Core.Messages;

public abstract class Event : Message, INotification
{
}
