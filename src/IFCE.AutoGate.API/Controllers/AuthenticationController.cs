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
    public async Task<IActionResult> ChangePassword(ChangePasswordAdministratorCommand command)
    {
        var result = await _mediator.SendCommand(command);

        return NoContentResponse(result);
    }
}
