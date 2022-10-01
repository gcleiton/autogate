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

    public async Task<IResult> SendCommand(Command command)
    {
        return await _mediator.Send(command);
    }

    public async Task PublishEvent(Event message)
    {
        await _mediator.Publish(message);
    }
}
