using IFCE.AutoGate.Application.UseCases.Authenticate;
using IFCE.AutoGate.Application.UseCases.ChangePassword;
using IFCE.AutoGate.Application.UseCases.RecoverPassword;
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
    public async Task<IActionResult> Authenticate(AuthenticateCommand command)
    {
        var result = await _mediator.SendCommand(command);

        return OkResponse(result);
    }

    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword(ChangePasswordCommand command)
    {
        await _mediator.SendCommand(command);

        return NoContentResponse();
    }

    [HttpPost("recover-password")]
    public async Task<IActionResult> RecoverPassword(RecoverPasswordCommand command)
    {
        await _mediator.SendCommand(command);

        return OkResponse($"Um e-mail foi enviado para {command.Email} contendo o link de recuperação da senha.");
    }
}
