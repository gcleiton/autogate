using IFCE.AutoGate.Application.UseCases.AddTransit;
using IFCE.AutoGate.Core.Contracts;
using IFCE.AutoGate.Domain.Contracts.Gateways;
using Microsoft.AspNetCore.Mvc;

namespace IFCE.AutoGate.API.Controllers;

[Route("api/transits")]
public class TransitController : BaseController
{
    private readonly IMediatorHandler _mediator;
    private readonly INotification _notification;

    public TransitController(INotification notification, IMediatorHandler mediator) : base(notification)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddTransitCommand command)
    {
        await _mediator.SendCommand(command);

        return CreatedResponse("", "Trânsito inserido com sucesso!");
    }
}
