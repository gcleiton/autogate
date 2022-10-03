using IFCE.AutoGate.Application.Commands;
using IFCE.AutoGate.Application.Requests;
using IFCE.AutoGate.Core.Contracts;
using IFCE.AutoGate.Domain.Contracts.Gateways;
using Microsoft.AspNetCore.Mvc;

namespace IFCE.AutoGate.API.Controllers;

[Route("api/auth")]
public class AuthenticationController : BaseController
{
    private readonly IMediatorHandler _mediator;
    private readonly INotification _notification;

    public AuthenticationController(INotification notification, IMediatorHandler mediator) : base(notification)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var accessToken = await _mediator.SendRequest(request);

        return OkResponse(accessToken);
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
