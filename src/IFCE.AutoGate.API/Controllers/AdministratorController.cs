using IFCE.AutoGate.Application.Commands;
using IFCE.AutoGate.Application.Queries;
using IFCE.AutoGate.Core.Contracts;
using IFCE.AutoGate.Domain.Contracts.Gateways;
using Microsoft.AspNetCore.Mvc;

namespace IFCE.AutoGate.API.Controllers;

[Route("api/administrators")]
public class AdministratorController : BaseController
{
    private readonly IMediatorHandler _mediator;
    private readonly INotification _notification;

    public AdministratorController(IMediatorHandler mediator, INotification notification) : base(notification)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateAdministratorCommand command)
    {
        var result = await _mediator.SendCommand(command);

        return CreatedResponse(result, "",
            $"Um e-mail foi enviado para o administrador {command.Name} contendo o link para o primeiro acesso no sistema.");
    }

    [HttpGet]
    public async Task<IActionResult> LoadAll([FromQuery] LoadAdministratorsQuery query)
    {
        var result = await _mediator.SendQuery(query);

        return OkResponse(result);
    }
}
