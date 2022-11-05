using IFCE.AutoGate.Application.UseCases.CreateDriver;
using IFCE.AutoGate.Application.UseCases.DeleteDriver;
using IFCE.AutoGate.Application.UseCases.ReactivateDriver;
using IFCE.AutoGate.Core.Contracts;
using IFCE.AutoGate.Domain.Contracts.Gateways;
using Microsoft.AspNetCore.Mvc;

namespace IFCE.AutoGate.API.Controllers;

[Route("api/drivers")]
public class DriverController : BaseController
{
    private readonly IMediatorHandler _mediator;
    private readonly INotification _notification;

    public DriverController(IMediatorHandler mediator, INotification notification) : base(notification)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateDriverCommand command)
    {
        await _mediator.SendCommand(command);

        return CreatedResponse("",
            $"Um e-mail foi enviado para o administrador {command.Name} contendo o link para o primeiro acesso no sistema.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.SendCommand(new DeleteDriverCommand(id));

        return NoContentResponse();
    }

    [HttpPatch("{id}/reactivate")]
    public async Task<IActionResult> Reactivate(Guid id)
    {
        await _mediator.SendCommand(new ReactivateDriverCommand(id));

        return NoContentResponse();
    }
}
