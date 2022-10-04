using IFCE.AutoGate.Application.Commands;
using IFCE.AutoGate.Domain.Contracts.Gateways;
using Microsoft.AspNetCore.Mvc;

namespace IFCE.AutoGate.API.Controllers;

[Route("api/administrators")]
public class AdministratorController : BaseController
{
    private readonly IMediatorHandler _mediator;

    public AdministratorController(IMediatorHandler mediator)
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
}
