using IFCE.AutoGate.Application.Commands;
using IFCE.AutoGate.Domain.Contracts.Gateways;
using Microsoft.AspNetCore.Mvc;

namespace IFCE.AutoGate.API.Controllers;

[Route("api/auth")]
public class AuthenticationController : BaseController
{
    private readonly IMediatorHandler _mediator;

    public AuthenticationController(IMediatorHandler mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword(ChangePasswordCommand command)
    {
        var result = await _mediator.SendCommand(command);

        return NoContentResponse(result);
    }

    [HttpPost("recover-password")]
    public async Task<IActionResult> RecoverPassword(RecoverPasswordCommand command)
    {
        await _mediator.SendCommand(command);

        return OkResponse($"Um e-mail foi enviado para {command.Email} contendo o link de recuperação da senha.");
    }
}
