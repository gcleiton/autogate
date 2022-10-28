using IFCE.AutoGate.Core.Messages;

namespace IFCE.AutoGate.Domain.Contracts.Gateways;

public interface IMediatorHandler
{
    Task<T> SendCommand<T>(Command<T> command);
    Task<TResponse> SendQuery<TResponse>(Query<TResponse> query);
    Task PublishEvent(Event message);
}
