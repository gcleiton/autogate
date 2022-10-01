using FluentValidation;
using IFCE.AutoGate.Application.Events;
using IFCE.AutoGate.Core.Communication;
using IFCE.AutoGate.Core.Contracts;
using IFCE.AutoGate.Core.Messages;
using IFCE.AutoGate.Domain.Contracts.Repositories;
using IFCE.AutoGate.Domain.Entities;
using IFCE.AutoGate.Domain.Errors;
using MediatR;

namespace IFCE.AutoGate.Application.Commands.Handlers;

public class CreateAdministratorCommandHandler : CommandHandler<CreateAdministratorCommand>,
    IRequestHandler<CreateAdministratorCommand, IResult>
{
    private readonly IAdministratorRepository _administratorRepository;

    public CreateAdministratorCommandHandler(IValidator<CreateAdministratorCommand> validator,
        IAdministratorRepository administratorRepository) : base(validator)
    {
        _administratorRepository = administratorRepository;
    }

    public async Task<IResult> Handle(CreateAdministratorCommand command, CancellationToken cancellationToken)
    {
        var errors = Validate(command);
        if (errors.Any()) return Result.Failure(new ValidationError(errors));

        var exists = await _administratorRepository.CheckByEmail(command.Email);
        if (exists)
            return Result.Failure(new AlreadyExistsError("O e-mail informado já está em uso por outro administrador"));

        var administrator = new Administrator(command.Name, command.Email);
        administrator.ForgetPassword();

        var createdEvent = new AdministratorCreatedEvent(administrator.Name, administrator.Email,
            administrator.RecoveryPasswordCode ?? Guid.Empty);
        administrator.AddNotification(createdEvent);

        _administratorRepository.Add(administrator);
        await _administratorRepository.UnitOfWork.Commit();

        command.AggregateId = administrator.Id;
        return Result.Ok();
    }
}
