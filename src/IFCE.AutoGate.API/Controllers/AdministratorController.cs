using IFCE.AutoGate.Application.UseCases.CreateAdministrator;
using IFCE.AutoGate.Application.UseCases.DeleteAdministrator;
using IFCE.AutoGate.Application.UseCases.LoadAdministratorById;
using IFCE.AutoGate.Application.UseCases.LoadAdministrators;
using IFCE.AutoGate.Application.UseCases.UpdateAdministrator;
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
        await _mediator.SendCommand(command);

        return CreatedResponse("",
            $"Um e-mail foi enviado para o administrador {command.Name} contendo o link para o primeiro acesso no sistema.");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateAdministratorCommand command)
    {
        command.Id = id;
        await _mediator.SendCommand(command);

        return CreatedResponse("",
            $"O administrador {command.Name} foi atualizado com sucesso.");
    }

    [HttpGet]
    public async Task<IActionResult> LoadAll([FromQuery] LoadAdministratorsQuery query)
    {
        var result = await _mediator.SendQuery(query);

        return OkResponse(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> LoadById(Guid id)
    {
        var result = await _mediator.SendQuery(new LoadAdministratorByIdQuery(id));

        return OkResponse(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.SendCommand(new DeleteAdministratorCommand(id));

        return NoContentResponse();
    }
}
