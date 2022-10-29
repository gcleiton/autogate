using FluentValidation;
using IFCE.AutoGate.Application.Events.AdministradorCreated;
using IFCE.AutoGate.Core.Messages;
using IFCE.AutoGate.Domain.Contracts.Repositories;
using IFCE.AutoGate.Domain.Entities;
using IFCE.AutoGate.Domain.Errors;
using MediatR;
using INotification = IFCE.AutoGate.Core.Contracts.INotification;

namespace IFCE.AutoGate.Application.UseCases.CreateAdministrator;

public class CreateAdministratorCommandHandler : CommandHandler<CreateAdministratorCommand>,
    IRequestHandler<CreateAdministratorCommand, bool>
{
    private readonly IAdministratorRepository _administratorRepository;

    public CreateAdministratorCommandHandler(INotification notification,
        IValidator<CreateAdministratorCommand> validator,
        IAdministratorRepository administratorRepository) : base(validator, notification)
    {
        _administratorRepository = administratorRepository;
    }

    public async Task<bool> Handle(CreateAdministratorCommand command, CancellationToken cancellationToken)
    {
        var errors = Validate(command);
        if (errors.Any()) return Failure(new ValidationError(errors));

        var exists = await _administratorRepository.CheckByEmail(command.Email);
        if (exists) return Failure(new AlreadyExistsError("O e-mail informado já está em uso por outro administrador"));

        var administrator = new Administrator(command.Name, command.Email);
        administrator.ForgetPassword();

        var createdEvent = new AdministratorCreatedEvent(administrator.Name, administrator.Email,
            administrator.RecoveryPasswordCode ?? Guid.Empty);
        administrator.AddEvent(createdEvent);

        _administratorRepository.Add(administrator);
        await _administratorRepository.UnitOfWork.Commit();

        command.AggregateId = administrator.Id;
        return true;
    }
}
