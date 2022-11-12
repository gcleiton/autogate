using IFCE.AutoGate.Application.UseCases.CreateDriver;
using IFCE.AutoGate.Application.UseCases.DeleteDriver;
using IFCE.AutoGate.Application.UseCases.LoadDriverById;
using IFCE.AutoGate.Application.UseCases.LoadDrivers;
using IFCE.AutoGate.Application.UseCases.ReactivateDriver;
using IFCE.AutoGate.Application.UseCases.UpdateDriver;
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

    [HttpGet]
    public async Task<IActionResult> LoadAll([FromQuery] LoadDriversQuery query)
    {
        var result = await _mediator.SendQuery(query);

        return OkResponse(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> LoadById(Guid id)
    {
        var result = await _mediator.SendQuery(new LoadDriverByIdQuery(id));

        return OkResponse(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateDriverCommand command)
    {
        await _mediator.SendCommand(command);

        return CreatedResponse("",
            $"Um e-mail foi enviado para o administrador {command.Name} contendo o link para o primeiro acesso no sistema.");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromForm] UpdateDriverCommand command)
    {
        command.Id = id;
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
