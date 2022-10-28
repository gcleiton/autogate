using IFCE.AutoGate.Core.Contracts;
using IFCE.AutoGate.Core.Messages;
using IFCE.AutoGate.Domain.Contracts.Gateways;
using MediatR;

namespace IFCE.AutoGate.Infrastructure.Gateways;

public class MediatorHandler : IMediatorHandler
{
    private readonly IMediator _mediator;

    public MediatorHandler(IMediator mediator)
    {
        _mediator = mediator;
    }


    public async Task<T> SendCommand<T>(Command<T> command)
    {
        return await _mediator.Send(command);
    }

    public async Task<TResponse> SendQuery<TEntity, TResponse>(Query<TEntity, TResponse> query) where TEntity : IEntity
    {
        return await _mediator.Send(query);
    }

    public async Task PublishEvent(Event message)
    {
        await _mediator.Publish(message);
    }
}
