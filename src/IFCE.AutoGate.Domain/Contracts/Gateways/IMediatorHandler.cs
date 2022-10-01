using IFCE.AutoGate.Core.Contracts;
using IFCE.AutoGate.Core.Messages;

namespace IFCE.AutoGate.Domain.Contracts.Gateways;

public interface IMediatorHandler
{
    Task<IResult> SendCommand(Command command);
    Task PublishEvent(Event message);
}
