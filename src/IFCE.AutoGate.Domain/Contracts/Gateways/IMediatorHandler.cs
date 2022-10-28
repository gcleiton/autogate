using IFCE.AutoGate.Core.Contracts;
using IFCE.AutoGate.Core.Messages;

namespace IFCE.AutoGate.Domain.Contracts.Gateways;

public interface IMediatorHandler
{
    Task<T> SendCommand<T>(Command<T> command);
    Task<TResponse> SendQuery<TEntity, TResponse>(Query<TEntity, TResponse> query) where TEntity : IEntity;
    Task PublishEvent(Event message);
}
