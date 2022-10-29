using FluentValidation;
using IFCE.AutoGate.Core.Messages;
using IFCE.AutoGate.Domain.Contracts.Repositories;
using IFCE.AutoGate.Domain.Errors;
using MediatR;
using INotification = IFCE.AutoGate.Core.Contracts.INotification;

namespace IFCE.AutoGate.Application.UseCases.UpdateAdministrator;

public class UpdateAdministratorCommandHandler : CommandHandler<UpdateAdministratorCommand>,
    IRequestHandler<UpdateAdministratorCommand, bool>
{
    private readonly IAdministratorRepository _administratorRepository;

    public UpdateAdministratorCommandHandler(IValidator<UpdateAdministratorCommand> validator,
        INotification notification, IAdministratorRepository administratorRepository) : base(validator, notification)
    {
        _administratorRepository = administratorRepository;
    }

    public async Task<bool> Handle(UpdateAdministratorCommand command, CancellationToken cancellationToken)
    {
        var errors = Validate(command);
        if (errors.Any())
        {
            AddError(new ValidationError(errors));
            return false;
        }

        var administrator = await _administratorRepository.LoadById(command.Id);

        if (administrator == null)
        {
            AddError(new NotFoundError("Administrador não encontrado"));
            return false;
        }

        var administratorExists =
            await _administratorRepository.Any(a => a.Email == command.Email && a.Id != command.Id);

        if (administratorExists)
        {
            AddError(new AlreadyExistsError("O e-mail informado já está em uso por outro administrador"));
            return false;
        }

        administrator.Rename(command.Name);
        administrator.ChangeEmail(command.Email);

        var isSuccess = await _administratorRepository.UnitOfWork.Commit();

        if (!isSuccess)
        {
            AddError(new UnexpectedError());
            return false;
        }

        return true;
    }
}
